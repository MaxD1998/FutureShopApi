﻿using Authorization.Core.Cqrs.RefreshToken.Commands;
using Authorization.Core.Cqrs.RefreshToken.Queries;
using Authorization.Core.Cqrs.User.Commands;
using Authorization.Core.Cqrs.User.Queries;
using Authorization.Core.Dtos.Login;
using Authorization.Core.Dtos.User;
using Authorization.Core.Services;
using Authorization.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Moq;
using Shared.Core.Dtos;
using Shared.Domain.Enums;
using Shared.Infrastructure.Constants;
using Shared.Infrastructure.Settings;
using System.Net;
using System.Security.Claims;

namespace Authorization.Test.Core.Services;

public class AuthServiceTest
{
    private readonly Mock<IHttpContextAccessor> _accessorMock = new();
    private readonly IAuthService _authService;
    private readonly Mock<ICookieService> _cookieServiceMock = new();
    private readonly Mock<IOptions<JwtSettings>> _jwtSettingsMock = new();
    private readonly Mock<IMediator> _mediatorMock = new();
    private readonly Mock<IOptions<RefreshTokenSettings>> _refreshTokenSettingsMock = new();

    public AuthServiceTest()
    {
        var jwtSettings = new JwtSettings()
        {
            ExpireTime = 1,
            JwtKey = $"{Guid.NewGuid()}-{Guid.NewGuid()}",
        };

        var refreshTokenSettings = new RefreshTokenSettings()
        {
            ExpireTime = 1
        };

        _jwtSettingsMock.Setup(x => x.Value).Returns(jwtSettings);
        _refreshTokenSettingsMock.Setup(x => x.Value).Returns(refreshTokenSettings);

        _authService = new AuthService(_cookieServiceMock.Object, _accessorMock.Object, _mediatorMock.Object, _jwtSettingsMock.Object, _refreshTokenSettingsMock.Object);
    }

    [Fact]
    public async Task LoginAsync_LoginFail_ReturnForbiddenError()
    {
        // Arrange
        _mediatorMock.Setup(x => x.Send(It.IsAny<GetUserEntityByEmailQuery>(), default)).ReturnsAsync((UserEntity)null);

        // Act
        var result = await _authService.LoginAsync(new LoginFormDto(), default);

        //Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(HttpStatusCode.Forbidden, result.HttpCode);
    }

    [Fact]
    public async Task LoginAsync_LoginSuccess_ReturnValue()
    {
        // Arrange
        var userEntity = new UserEntity()
        {
            Id = Guid.NewGuid(),
            Email = "test@futureshop.pl",
            HashedPassword = "HashedPassword",
            FirstName = "Test",
            LastName = "User",
        };

        var refreshTokenEntity = new RefreshTokenEntity()
        {
            Id = Guid.NewGuid(),
            UserId = userEntity.Id,
            User = userEntity,
            StartDate = DateOnly.FromDateTime(DateTime.UtcNow),
            EndDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(_refreshTokenSettingsMock.Object.Value.ExpireTime)),
            Token = Guid.NewGuid()
        };

        _mediatorMock.Setup(x => x.Send(It.IsAny<GetUserEntityByEmailQuery>(), default)).ReturnsAsync(userEntity);
        _mediatorMock.Setup(x => x.Send(It.IsAny<CreateOrUpdateRefreshTokenEntityByUserIdCommand>(), default)).ReturnsAsync(refreshTokenEntity);

        // Act
        var result = await _authService.LoginAsync(new LoginFormDto(), default);

        //Assert
        _cookieServiceMock.Verify(x
            => x.AddCookie(
                It.Is<string>(y => y == CookieNameConst.RefreshToken),
                It.Is<string>(y => y == refreshTokenEntity.Token.ToString()),
                It.Is<int>(y => y == _refreshTokenSettingsMock.Object.Value.ExpireTime)
            )
        , Times.Once);

        Assert.NotNull(result);
        Assert.Equal(userEntity.Id, result.Result.Id);
        Assert.Equal($"{userEntity.FirstName} {userEntity.LastName}", result.Result.Username);
        Assert.NotEmpty(result.Result.Token);
    }

    [Fact]
    public async Task LogoutAsync_LogoutFaild_ReturnBadRequestError()
    {
        // Arrange
        var claimsPrincipalMock = new Mock<ClaimsPrincipal>();
        var httpContextMock = new DefaultHttpContext();
        var accessorMock = new Mock<IHttpContextAccessor>();

        claimsPrincipalMock.Setup(x => x.Claims).Returns([]);
        httpContextMock.User = claimsPrincipalMock.Object;
        accessorMock.Setup(x => x.HttpContext).Returns(httpContextMock);

        var authService = new AuthService(_cookieServiceMock.Object, accessorMock.Object, _mediatorMock.Object, _jwtSettingsMock.Object, _refreshTokenSettingsMock.Object);

        // Act
        var result = await authService.LogoutAsync(default);

        //Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(HttpStatusCode.BadRequest, result.HttpCode);
    }

    [Fact]
    public async Task RefreshTokenAsync_CookieValueIsEmpty_ReturnNull()
    {
        // Arrange
        _cookieServiceMock.Setup(x => x.GetCookieValue(CookieNameConst.RefreshToken)).Returns(ResultDto.Success(string.Empty));

        // Act
        var result = await _authService.RefreshTokenAsync();

        //Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task RefreshTokenAsync_RefreshJwtSuccess_ReturnValue()
    {
        // Arrange
        var userEntity = new UserEntity()
        {
            Id = Guid.NewGuid(),
            Email = "test@futureshop.pl",
            HashedPassword = "HashedPassword",
            FirstName = "Test",
            LastName = "User",
            Type = UserType.SuperAdmin
        };

        var refreshTokenEntity = new RefreshTokenEntity()
        {
            Id = Guid.NewGuid(),
            UserId = userEntity.Id,
            User = userEntity,
            StartDate = DateOnly.FromDateTime(DateTime.UtcNow),
            EndDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(_refreshTokenSettingsMock.Object.Value.ExpireTime)),
            Token = Guid.NewGuid(),
        };

        _cookieServiceMock.Setup(x => x.GetCookieValue(CookieNameConst.RefreshToken)).Returns(ResultDto.Success(Guid.NewGuid().ToString()));
        _mediatorMock.Setup(x => x.Send(It.IsAny<GetRefereshTokenEntityByTokenQuery>(), default)).ReturnsAsync(refreshTokenEntity);

        // Act
        var result = await _authService.RefreshTokenAsync();

        //Assert
        Assert.NotNull(result);
        Assert.Equal(userEntity.Id, result.Result.Id);
        Assert.Equal($"{userEntity.FirstName} {userEntity.LastName}", result.Result.Username);
        Assert.NotEmpty(result.Result.Token);
        Assert.Equal(userEntity.Type.GetUserPrivileges(), result.Result.Roles);
    }

    [Fact]
    public async Task RefreshTokenAsync_RefreshTokenWasNull_ThrowForbiddenException()
    {
        // Arrange
        _cookieServiceMock.Setup(x => x.GetCookieValue(CookieNameConst.RefreshToken)).Returns(ResultDto.Success(Guid.NewGuid().ToString()));
        _mediatorMock.Setup(x => x.Send(It.IsAny<GetRefereshTokenEntityByTokenQuery>(), default)).ReturnsAsync((RefreshTokenEntity)null);

        // Act
        var result = await _authService.RefreshTokenAsync();

        //Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(HttpStatusCode.Forbidden, result.HttpCode);
    }

    [Fact]
    public async Task RefreshTokenAsync_WrongRefreshTokenFormat_ThrowForbiddenException()
    {
        // Arrange
        _cookieServiceMock.Setup(x => x.GetCookieValue(CookieNameConst.RefreshToken)).Returns(ResultDto.Success("Random string"));

        // Act
        var result = await _authService.RefreshTokenAsync();

        //Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(HttpStatusCode.Forbidden, result.HttpCode);
    }

    [Fact]
    public async Task RegisterAsync_RegisterSuccess_ReturnValue()
    {
        // Arrange
        var userEntity = new UserEntity()
        {
            Id = Guid.NewGuid(),
            Email = "test@futureshop.pl",
            HashedPassword = "HashedPassword",
            FirstName = "Test",
            LastName = "User",
        };

        var refreshTokenEntity = new RefreshTokenEntity()
        {
            Id = Guid.NewGuid(),
            UserId = userEntity.Id,
            User = userEntity,
            StartDate = DateOnly.FromDateTime(DateTime.UtcNow),
            EndDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(_refreshTokenSettingsMock.Object.Value.ExpireTime)),
            Token = Guid.NewGuid()
        };

        _mediatorMock.Setup(x => x.Send(It.IsAny<CreateUserEntityCommand>(), default)).ReturnsAsync(ResultDto.Success(userEntity));
        _mediatorMock.Setup(x => x.Send(It.IsAny<CreateOrUpdateRefreshTokenEntityByUserIdCommand>(), default)).ReturnsAsync(refreshTokenEntity);

        // Act
        var result = await _authService.RegisterAsync(new UserFormDto(), default);

        //Assert
        _cookieServiceMock.Verify(x
            => x.AddCookie(
                It.Is<string>(y => y == CookieNameConst.RefreshToken),
                It.Is<string>(y => y == refreshTokenEntity.Token.ToString()),
                It.Is<int>(y => y == _refreshTokenSettingsMock.Object.Value.ExpireTime)
            )
        , Times.Once);

        Assert.NotNull(result);
        Assert.Equal(userEntity.Id, result.Result.Id);
        Assert.Equal($"{userEntity.FirstName} {userEntity.LastName}", result.Result.Username);
        Assert.NotEmpty(result.Result.Token);
    }
}