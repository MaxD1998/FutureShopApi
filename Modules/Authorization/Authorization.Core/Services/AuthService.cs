using Authorization.Core.Dtos;
using Authorization.Core.Dtos.Users;
using Authorization.Domain.Aggregates.Users;
using Authorization.Domain.Aggregates.Users.Entities;
using Authorization.Inrfrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Core.Errors;
using Shared.Domain.Enums;
using Shared.Infrastructure.Constants;
using Shared.Infrastructure.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Authorization.Core.Services;

public interface IAuthService
{
    Task<ResultDto<AuthorizeDto>> LoginAsync(LoginFormDto dto, CancellationToken cancellationToken = default);

    Task<ResultDto> LogoutAsync(CancellationToken cancellationToken = default);

    Task<ResultDto<AuthorizeDto>> RefreshTokenAsync(CancellationToken cancellationToken = default);

    Task<ResultDto<AuthorizeDto>> RegisterAsync(UserFormDto dto, CancellationToken cancellationToken = default);
}

public class AuthService : BaseService, IAuthService
{
    private readonly ICookieService _cookieService;
    private readonly HttpContext _httpContext;
    private readonly JwtSettings _jwtSettings;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly RefreshTokenSettings _refreshTokenSettings;
    private readonly IUserRepository _userRepository;

    public AuthService(
        IHttpContextAccessor accessor,
        ICookieService cookieService,
        IOptions<JwtSettings> jwtSettings,
        IRefreshTokenRepository refreshTokenRepository,
        IOptions<RefreshTokenSettings> refreshTokenSettings,
        IUserRepository userRepository)
    {
        _cookieService = cookieService;
        _httpContext = accessor.HttpContext;
        _jwtSettings = jwtSettings.Value;
        _refreshTokenRepository = refreshTokenRepository;
        _refreshTokenSettings = refreshTokenSettings.Value;
        _userRepository = userRepository;
    }

    public async Task<ResultDto<AuthorizeDto>> LoginAsync(LoginFormDto dto, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByEmailAsync(dto.Email, cancellationToken);

        if (user is null)
            return Error<AuthorizeDto>(HttpStatusCode.Forbidden, CommonExceptionMessage.C004WrongEmailOrPassword);

        var refreshToken = await AddOrUpdateRefreshTokenAsync(user.Id, cancellationToken);
        var result = new AuthorizeDto(user, GenerateJwt(user));
        var expireDays = _refreshTokenSettings.ExpireTime;

        _cookieService.AddCookie(CookieNameConst.RefreshToken, refreshToken.ToString(), expireDays);

        return Success(result);
    }

    public async Task<ResultDto> LogoutAsync(CancellationToken cancellationToken = default)
    {
        var nullableUserId = _httpContext.User.Claims.FirstOrDefault(x => x.Type == JwtClaimNameConst.Id)?.Value;

        if (!Guid.TryParse(nullableUserId, out var userId))
            return Error(HttpStatusCode.BadRequest, CommonExceptionMessage.C005BadGuidFormat);

        await _refreshTokenRepository.DeleteByUserId(userId, cancellationToken);

        _cookieService.RemoveCookie(CookieNameConst.RefreshToken);

        return Success();
    }

    public async Task<ResultDto<AuthorizeDto>> RefreshTokenAsync(CancellationToken cancellationToken = default)
    {
        var userRefreshToken = _cookieService.GetCookieValue(CookieNameConst.RefreshToken).Result;

        if (string.IsNullOrEmpty(userRefreshToken))
            return Success<AuthorizeDto>(null);

        if (!Guid.TryParse(userRefreshToken, out var token))
            return Error<AuthorizeDto>(HttpStatusCode.Forbidden, CommonExceptionMessage.C003WrongRefreshTokenFormat);

        var user = await _userRepository.GetByTokenAsync(token, cancellationToken);

        if (user is null)
        {
            _cookieService.RemoveCookie(CookieNameConst.RefreshToken);
            return Error<AuthorizeDto>(HttpStatusCode.Forbidden, CommonExceptionMessage.C001SessionHasExpired);
        }

        return Success(new AuthorizeDto(user, GenerateJwt(user)));
    }

    public async Task<ResultDto<AuthorizeDto>> RegisterAsync(UserFormDto dto, CancellationToken cancellationToken = default)
    {
        var isExist = await _userRepository.AnyByEmailAsync(dto.Email, cancellationToken);

        if (isExist)
            return Error<AuthorizeDto>(HttpStatusCode.Conflict, CommonExceptionMessage.C006RecordAlreadyExists);

        var user = await _userRepository.CreateAsync(dto.ToEntity(), cancellationToken);
        var refreshToken = await AddOrUpdateRefreshTokenAsync(user.Id, cancellationToken);
        var result = new AuthorizeDto(user, GenerateJwt(user));
        var expireDays = _refreshTokenSettings.ExpireTime;

        _cookieService.AddCookie(CookieNameConst.RefreshToken, refreshToken.ToString(), expireDays);

        return Success(result);
    }

    private static List<Claim> GetClaims(User user)
    {
        var result = new List<Claim>()
        {
            new(JwtClaimNameConst.Id, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(JwtRegisteredClaimNames.Name, user.FirstName),
            new(JwtRegisteredClaimNames.FamilyName, user.LastName),
            new(JwtClaimNameConst.Role, user.Type.ToString()),
        };

        foreach (var type in user.Type.GetUserPrivileges())
            result.Add(new Claim(JwtClaimNameConst.Role, type.ToString()));

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

    private string GenerateJwt(User user)
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