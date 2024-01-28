using Core.Cqrs.RefreshToken.Commands;
using Core.Cqrs.RefreshToken.Queries;
using Core.Cqrs.User.Queries;
using Core.Dtos;
using Core.Dtos.Login;
using Core.Dtos.RefreshTokes;
using Core.Interfaces.Services;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shared.Constants;
using Shared.Dtos.Settings;
using Shared.Errors;
using Shared.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Core.Services;

public class AuthService : IAuthService
{
    private readonly ICookieService _cookieService;
    private readonly JwtSettings _jwtSettings;
    private readonly IMediator _mediator;
    private readonly RefreshTokenSettings _refreshTokenSettings;

    public AuthService(
        ICookieService cookieService,
        IMediator mediator,
        IOptions<JwtSettings> jwtSettings,
        IOptions<RefreshTokenSettings> refreshTokenSettings)
    {
        _cookieService = cookieService;
        _jwtSettings = jwtSettings.Value;
        _mediator = mediator;
        _refreshTokenSettings = refreshTokenSettings.Value;
    }

    public async Task<AuthorizeDto> LoginAsync(LoginDto dto)
    {
        var user = await _mediator.Send(new GetUserEntityByEmailQuery(dto.Email));
        var refreshToken = await AddOrUpdateRefreshTokenAsync(user.Id);
        var result = new AuthorizeDto()
        {
            Id = user.Id,
            Username = $"{user.FirstName} {user.LastName}",
            Token = GenerateJwt(user),
        };

        var expireDays = _refreshTokenSettings.ExpireTime;

        _cookieService.AddCookie(CookieNameConst.RefreshToken, refreshToken.ToString(), expireDays, true);

        return result;
    }

    public Task LogoutAsync()
    {
        _cookieService.RemoveCookie(CookieNameConst.RefreshToken);

        return Task.CompletedTask;
    }

    public async Task<AuthorizeDto> RefreshTokenAsync()
    {
        var userRefreshToken = _cookieService.GetCookie(CookieNameConst.RefreshToken);

        if (!Guid.TryParse(userRefreshToken, out var token))
            throw new ForbiddenException(ExceptionMessage.WrongRefreshTokenFormat);

        var refreshToken = await _mediator.Send(new GetRefereshTokenEntityByTokenQuery(token));

        if (refreshToken is null)
            throw new ForbiddenException(ExceptionMessage.SessionHasExpired);

        var user = refreshToken.User;

        return new AuthorizeDto()
        {
            Id = user.Id,
            Username = $"{user.FirstName} {user.LastName}",
            Token = GenerateJwt(user),
        };
    }

    private async Task<Guid> AddOrUpdateRefreshTokenAsync(Guid userId)
    {
        var inputRefreshToken = new RefreshTokenInputDto()
        {
            StartDate = DateOnly.FromDateTime(DateTime.UtcNow),
            EndDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(_refreshTokenSettings.ExpireTime)),
            Token = Guid.NewGuid(),
            UserId = userId,
        };

        var refreshToken = await _mediator.Send(new CreateOrUpdateRefreshTokenEntityByUserIdCommand(userId, inputRefreshToken));
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

    private List<Claim> GetClaims(UserEntity user) => new()
    {
        new Claim(JwtClaimNameConst.Id, user.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim(JwtRegisteredClaimNames.Name, user.FirstName),
        new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
        new Claim(JwtClaimNameConst.Role, user.Type.ToString()),
    };
}