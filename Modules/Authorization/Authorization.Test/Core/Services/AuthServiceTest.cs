using Authorization.Core.Dtos;
using Authorization.Core.Dtos.Users;
using Authorization.Core.Services;
using Authorization.Domain.Aggregates.Users;
using Authorization.Infrastructure.Repositories;
using Authorization.Test.Shared.Factories;
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
    private readonly Mock<IOptions<RefreshTokenSettings>> _refreshTokenSettingsMock = new();
    private readonly Mock<IUserRepository> _userRepositoryMock = new();

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

        _authService = new AuthService(_accessorMock.Object, _cookieServiceMock.Object, _jwtSettingsMock.Object, _refreshTokenSettingsMock.Object, _userRepositoryMock.Object);
    }

    [Fact]
    public async Task LoginAsync_LoginFail_ReturnForbiddenError()
    {
        // Arrange
        _userRepositoryMock.Setup(x => x.GetByEmailAsync(default, default)).ReturnsAsync((UserAggregate)null);

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
        var user = UserTestFactory.Create();
        var refreshToken = UserTestFactory.CreateRefreshToken(_refreshTokenSettingsMock.Object.Value.ExpireTime);
        user.SetRefreshToken(refreshToken);

        var login = new LoginFormDto()
        {
            Email = "test@futureshop.pl",
            Password = "password",
        };

        _userRepositoryMock.Setup(x => x.GetByEmailAsync(It.IsAny<string>(), default)).ReturnsAsync(user);
        _userRepositoryMock.Setup(x => x.UpdateAsync(user.Id, It.IsAny<UserAggregate>(), default)).ReturnsAsync(user);

        // Act
        var result = await _authService.LoginAsync(login, default);

        //Assert
        _cookieServiceMock.Verify(x
            => x.AddCookie(
                It.Is<string>(y => y == CookieNameConst.RefreshToken),
                It.Is<string>(y => y == user.RefreshToken.Token.ToString()),
                It.Is<int>(y => y == _refreshTokenSettingsMock.Object.Value.ExpireTime)
            )
        , Times.Once);

        Assert.NotNull(result);
        Assert.Equal(user.Id, result.Result.Id);
        Assert.Equal($"{user.FirstName} {user.LastName}", result.Result.Username);
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

        var authService = new AuthService(accessorMock.Object, _cookieServiceMock.Object, _jwtSettingsMock.Object, _refreshTokenSettingsMock.Object, _userRepositoryMock.Object);

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
        Assert.Null(result.Result);
    }

    [Fact]
    public async Task RefreshTokenAsync_RefreshJwtSuccess_ReturnValue()
    {
        // Arrange
        var user = UserTestFactory.Create();
        user.SetType(UserType.SuperAdmin);

        _cookieServiceMock.Setup(x => x.GetCookieValue(CookieNameConst.RefreshToken)).Returns(ResultDto.Success(Guid.NewGuid().ToString()));
        _userRepositoryMock.Setup(x => x.GetByTokenAsync(It.IsAny<Guid>(), default)).ReturnsAsync(user);

        // Act
        var result = await _authService.RefreshTokenAsync();

        //Assert
        Assert.NotNull(result);
        Assert.Equal(user.Id, result.Result.Id);
        Assert.Equal($"{user.FirstName} {user.LastName}", result.Result.Username);
        Assert.NotEmpty(result.Result.Token);
        Assert.Equal(user.Type.GetUserPrivileges(), result.Result.Roles);
    }

    [Fact]
    public async Task RefreshTokenAsync_RefreshTokenWasNull_ThrowForbiddenException()
    {
        // Arrange
        _cookieServiceMock.Setup(x => x.GetCookieValue(CookieNameConst.RefreshToken)).Returns(ResultDto.Success(Guid.NewGuid().ToString()));
        _userRepositoryMock.Setup(x => x.GetByTokenAsync(Guid.NewGuid(), default)).ReturnsAsync((UserAggregate)null);

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
        var user = UserTestFactory.Create();
        var refreshToken = UserTestFactory.CreateRefreshToken(_refreshTokenSettingsMock.Object.Value.ExpireTime);
        user.SetRefreshToken(refreshToken);

        var login = new UserFormDto()
        {
            Email = "test@futureshop.pl",
            Password = "HashedPassword",
            FirstName = "Test",
            LastName = "User",
        };

        _userRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<UserAggregate>(), default)).ReturnsAsync(user);
        _userRepositoryMock.Setup(x => x.UpdateAsync(user.Id, It.IsAny<UserAggregate>(), default)).ReturnsAsync(user);

        // Act
        var result = await _authService.RegisterAsync(login, default);

        //Assert
        _cookieServiceMock.Verify(x
            => x.AddCookie(
                It.Is<string>(y => y == CookieNameConst.RefreshToken),
                It.Is<string>(y => y == user.RefreshToken.Token.ToString()),
                It.Is<int>(y => y == _refreshTokenSettingsMock.Object.Value.ExpireTime)
            )
        , Times.Once);

        Assert.NotNull(result);
        Assert.Equal(user.Id, result.Result.Id);
        Assert.Equal($"{user.FirstName} {user.LastName}", result.Result.Username);
        Assert.NotEmpty(result.Result.Token);
    }
}