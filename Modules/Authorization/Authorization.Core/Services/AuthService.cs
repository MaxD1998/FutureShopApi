using Authorization.Core.Cqrs.RefreshToken.Commands;
using Authorization.Core.Cqrs.RefreshToken.Queries;
using Authorization.Core.Cqrs.User.Commands;
using Authorization.Core.Cqrs.User.Queries;
using Authorization.Core.Dtos;
using Authorization.Core.Dtos.Login;
using Authorization.Core.Dtos.RefreshToken;
using Authorization.Core.Dtos.User;
using Authorization.Domain.Entities;
using MediatR;
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
    private readonly IMediator _mediator;
    private readonly RefreshTokenSettings _refreshTokenSettings;

    public AuthService(
        ICookieService cookieService,
        IHttpContextAccessor accessor,
        IMediator mediator,
        IOptions<JwtSettings> jwtSettings,
        IOptions<RefreshTokenSettings> refreshTokenSettings)
    {
        _cookieService = cookieService;
        _httpContext = accessor.HttpContext;
        _jwtSettings = jwtSettings.Value;
        _mediator = mediator;
        _refreshTokenSettings = refreshTokenSettings.Value;
    }

    public async Task<ResultDto<AuthorizeDto>> LoginAsync(LoginFormDto dto, CancellationToken cancellationToken = default)
    {
        var user = await _mediator.Send(new GetUserEntityByEmailQuery(dto.Email), cancellationToken);

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

        await _mediator.Send(new DeleteRefreshTokenByUserIdCommand(userId), cancellationToken);

        _cookieService.RemoveCookie(CookieNameConst.RefreshToken);

        return Success();
    }

    public async Task<ResultDto<AuthorizeDto>> RefreshTokenAsync(CancellationToken cancellationToken = default)
    {
        var userRefreshToken = _cookieService.GetCookieValue(CookieNameConst.RefreshToken).Result;

        if (string.IsNullOrEmpty(userRefreshToken))
            return null;

        if (!Guid.TryParse(userRefreshToken, out var token))
            return Error<AuthorizeDto>(HttpStatusCode.Forbidden, CommonExceptionMessage.C003WrongRefreshTokenFormat);

        var refreshToken = await _mediator.Send(new GetRefereshTokenEntityByTokenQuery(token), cancellationToken);

        if (refreshToken is null)
        {
            _cookieService.RemoveCookie(CookieNameConst.RefreshToken);
            return Error<AuthorizeDto>(HttpStatusCode.Forbidden, CommonExceptionMessage.C001SessionHasExpired);
        }

        var user = refreshToken.User;

        return Success(new AuthorizeDto(user, GenerateJwt(user)));
    }

    public async Task<ResultDto<AuthorizeDto>> RegisterAsync(UserFormDto dto, CancellationToken cancellationToken = default)
    {
        var userResult = await _mediator.Send(new CreateUserEntityCommand(dto), cancellationToken);
        if (!userResult.IsSuccess)
            return Error<AuthorizeDto>(userResult.HttpCode, userResult.ErrorMessage);

        var user = userResult.Result;
        var refreshToken = await AddOrUpdateRefreshTokenAsync(user.Id, cancellationToken);
        var result = new AuthorizeDto(user, GenerateJwt(user));
        var expireDays = _refreshTokenSettings.ExpireTime;

        _cookieService.AddCookie(CookieNameConst.RefreshToken, refreshToken.ToString(), expireDays);

        return Success(result);
    }

    private static List<Claim> GetClaims(UserEntity user)
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
        var inputRefreshToken = new RefreshTokenFormDto()
        {
            StartDate = DateOnly.FromDateTime(DateTime.UtcNow),
            EndDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(_refreshTokenSettings.ExpireTime)),
            Token = Guid.NewGuid(),
            UserId = userId,
        };

        var refreshToken = await _mediator.Send(new CreateOrUpdateRefreshTokenEntityByUserIdCommand(userId, inputRefreshToken), cancellationToken);
        return refreshToken.Token;
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