using Authorization.Core.Dtos;
using Authorization.Core.Dtos.Login;
using Authorization.Core.Dtos.Register;
using Authorization.Core.Dtos.User;
using Authorization.Core.Errors;
using Authorization.Infrastructure.Entities.Users;
using Authorization.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shared.Core.Constans;
using Shared.Core.Dtos;
using Shared.Core.Enums;
using Shared.Core.Errors;
using Shared.Infrastructure;
using Shared.Infrastructure.Constants;
using Shared.Infrastructure.Enums;
using Shared.Infrastructure.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Crypt = BCrypt.Net.BCrypt;

namespace Authorization.Core.Services;

public interface IAuthService
{
    Task<ResultDto<AuthorizeDto>> LoginAsync(LoginFormDto dto, CancellationToken cancellationToken = default);

    Task<ResultDto> LogoutAsync(CancellationToken cancellationToken = default);

    Task<ResultDto<AuthorizeDto>> RefreshTokenAsync(CancellationToken cancellationToken = default);

    Task<ResultDto<AuthorizeDto>> RegisterAsync(RegisterFormDto dto, CancellationToken cancellationToken = default);
}

internal class AuthService : IAuthService
{
    private readonly ICookieService _cookieService;
    private readonly HttpContext _httpContext;
    private readonly JwtSettings _jwtSettings;
    private readonly IRabbitMqContext _rabbitMqContext;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly RefreshTokenSettings _refreshTokenSettings;
    private readonly IUserRepository _userRepository;

    public AuthService(
        IHttpContextAccessor accessor,
        ICookieService cookieService,
        IOptions<JwtSettings> jwtSettings,
        IRefreshTokenRepository refreshTokenRepository,
        IOptions<RefreshTokenSettings> refreshTokenSettings,
        IRabbitMqContext rabbitMqContext,
        IUserRepository userRepository)
    {
        _cookieService = cookieService;
        _httpContext = accessor.HttpContext;
        _jwtSettings = jwtSettings.Value;
        _refreshTokenRepository = refreshTokenRepository;
        _rabbitMqContext = rabbitMqContext;
        _refreshTokenSettings = refreshTokenSettings.Value;
        _userRepository = userRepository;
    }

    public async Task<ResultDto<AuthorizeDto>> LoginAsync(LoginFormDto dto, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByEmailAsync(dto.Email, cancellationToken);

        if (user is null || !Crypt.Verify(dto.Password, user?.HashedPassword))
            return ResultDto.Error<AuthorizeDto>(HttpStatusCode.Forbidden, ExceptionMessage.User001WrongEmailOrPassword);

        var refreshToken = await AddOrUpdateRefreshTokenAsync(user.Id, cancellationToken);
        var result = new AuthorizeDto(user, GenerateJwt(user));
        var expireDays = _refreshTokenSettings.ExpireTime;

        _cookieService.AddCookie(CookieNameConst.RefreshToken, refreshToken.ToString(), expireDays);

        return ResultDto.Success(result);
    }

    public async Task<ResultDto> LogoutAsync(CancellationToken cancellationToken = default)
    {
        var nullableUserId = _httpContext.User.Claims.FirstOrDefault(x => x.Type == JwtClaimNameConst.Id)?.Value;

        if (!Guid.TryParse(nullableUserId, out var userId))
            return ResultDto.Error(HttpStatusCode.BadRequest, CommonExceptionMessage.C002BadGuidFormat);

        await _refreshTokenRepository.DeleteByUserId(userId, cancellationToken);

        _cookieService.RemoveCookie(CookieNameConst.RefreshToken);

        return ResultDto.Success();
    }

    public async Task<ResultDto<AuthorizeDto>> RefreshTokenAsync(CancellationToken cancellationToken = default)
    {
        var userRefreshToken = _cookieService.GetCookieValue(CookieNameConst.RefreshToken).Result;

        if (string.IsNullOrEmpty(userRefreshToken))
            return ResultDto.Success<AuthorizeDto>(null);

        if (!Guid.TryParse(userRefreshToken, out var token))
            return ResultDto.Error<AuthorizeDto>(HttpStatusCode.Forbidden, ExceptionMessage.User002WrongRefreshTokenFormat);

        var user = await _userRepository.GetByTokenAsync(token, cancellationToken);

        if (user is null)
        {
            _cookieService.RemoveCookie(CookieNameConst.RefreshToken);
            return ResultDto.Error<AuthorizeDto>(HttpStatusCode.Forbidden, CommonExceptionMessage.C001SessionHasExpired);
        }

        return ResultDto.Success(new AuthorizeDto(user, GenerateJwt(user)));
    }

    public async Task<ResultDto<AuthorizeDto>> RegisterAsync(RegisterFormDto dto, CancellationToken cancellationToken = default)
    {
        var isExist = await _userRepository.AnyByEmailAsync(dto.Email, cancellationToken);

        if (isExist)
            return ResultDto.Error<AuthorizeDto>(HttpStatusCode.Conflict, CommonExceptionMessage.C003RecordAlreadyExists);

        var user = await _userRepository.CreateAsync(dto.ToEntity(), cancellationToken);
        var refreshToken = await AddOrUpdateRefreshTokenAsync(user.Id, cancellationToken);
        var result = new AuthorizeDto(user, GenerateJwt(user));
        var expireDays = _refreshTokenSettings.ExpireTime;

        await _rabbitMqContext.SendMessageAsync(RabbitMqExchangeConst.AuthorizationModuleUser, EventMessageDto.Create(new UserEventDto(user), MessageType.AddOrUpdate));

        _cookieService.AddCookie(CookieNameConst.RefreshToken, refreshToken.ToString(), expireDays);

        return ResultDto.Success(result);
    }

    private static List<Claim> GetClaims(UserEntity user)
    {
        var result = new List<Claim>()
        {
            new(JwtClaimNameConst.Id, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(JwtRegisteredClaimNames.Name, user.FirstName),
            new(JwtRegisteredClaimNames.FamilyName, user.LastName),
        };

        foreach (var role in user.Type.GetUserPrivileges())
            result.Add(new(JwtClaimNameConst.Role, role.ToString()));

        foreach (var permissionGroupId in user.UserPermissionGroups.Select(x => x.PermissionGroupId.ToString()))
            result.Add(new(JwtClaimNameConst.Permissions, permissionGroupId));

        return result;
    }

    private async Task<Guid> AddOrUpdateRefreshTokenAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var entity = new RefreshTokenEntity()
        {
            StartDate = DateOnly.FromDateTime(DateTime.UtcNow),
            EndDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(_refreshTokenSettings.ExpireTime)),
            Token = Guid.NewGuid(),
            UserId = userId,
        };

        entity = await _refreshTokenRepository.CreateOrUpdateByUserIdAsync(entity, cancellationToken);
        return entity.Token;
    }

    private string GenerateJwt(UserEntity user)
    {
        var claims = GetClaims(user);
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.JwtKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpireTime),
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var getToken = tokenHandler.WriteToken(token);

        return getToken;
    }
}