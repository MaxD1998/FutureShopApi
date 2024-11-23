using Authorization.Core.Cqrs.RefreshToken.Commands;
using Authorization.Core.Cqrs.RefreshToken.Queries;
using Authorization.Core.Cqrs.User.Commands;
using Authorization.Core.Cqrs.User.Queries;
using Authorization.Core.Dtos;
using Authorization.Core.Dtos.Login;
using Authorization.Core.Dtos.RefreshToken;
using Authorization.Core.Dtos.User;
using Authorization.Core.Interfaces.Services;
using Authorization.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shared.Core.Errors;
using Shared.Core.Exceptions;
using Shared.Domain.Enums;
using Shared.Infrastructure.Constants;
using Shared.Infrastructure.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Authorization.Core.Services;

public class AuthService : IAuthService
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

    public async Task<AuthorizeDto> LoginAsync(LoginFormDto dto, CancellationToken cancellationToken = default)
    {
        var user = await _mediator.Send(new GetUserEntityByEmailQuery(dto.Email), cancellationToken);

        if (user is null)
            throw new ForbiddenException(CommonExceptionMessage.C004WrongEmailOrPassword);

        var refreshToken = await AddOrUpdateRefreshTokenAsync(user.Id, cancellationToken);
        var result = new AuthorizeDto(user, GenerateJwt(user));
        var expireDays = _refreshTokenSettings.ExpireTime;

        _cookieService.AddCookie(CookieNameConst.RefreshToken, refreshToken.ToString(), expireDays);

        return result;
    }

    public async Task LogoutAsync(CancellationToken cancellationToken = default)
    {
        var nullableUserId = _httpContext.User.Claims.FirstOrDefault(x => x.Type == JwtClaimNameConst.Id)?.Value;

        if (!Guid.TryParse(nullableUserId, out var userId))
            throw new BadRequestException(CommonExceptionMessage.C005BadGuidFormat);

        await _mediator.Send(new DeleteRefreshTokenByUserIdCommand(userId), cancellationToken);

        _cookieService.RemoveCookie(CookieNameConst.RefreshToken);
    }

    public async Task<AuthorizeDto> RefreshTokenAsync(CancellationToken cancellationToken = default)
    {
        var userRefreshToken = _cookieService.GetCookieValue(CookieNameConst.RefreshToken);

        if (string.IsNullOrEmpty(userRefreshToken))
            return null;

        if (!Guid.TryParse(userRefreshToken, out var token))
            throw new ForbiddenException(CommonExceptionMessage.C003WrongRefreshTokenFormat);

        var refreshToken = await _mediator.Send(new GetRefereshTokenEntityByTokenQuery(token), cancellationToken);

        if (refreshToken is null)
        {
            _cookieService.RemoveCookie(CookieNameConst.RefreshToken);
            throw new ForbiddenException(CommonExceptionMessage.C001SessionHasExpired);
        }

        var user = refreshToken.User;

        return new AuthorizeDto(user, GenerateJwt(user));
    }

    public async Task<AuthorizeDto> RegisterAsync(UserFormDto dto, CancellationToken cancellationToken = default)
    {
        var user = await _mediator.Send(new CreateUserEntityCommand(dto), cancellationToken);
        var refreshToken = await AddOrUpdateRefreshTokenAsync(user.Id, cancellationToken);
        var result = new AuthorizeDto(user, GenerateJwt(user));
        var expireDays = _refreshTokenSettings.ExpireTime;

        _cookieService.AddCookie(CookieNameConst.RefreshToken, refreshToken.ToString(), expireDays);

        return result;
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