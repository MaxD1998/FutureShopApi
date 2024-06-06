using Authorization.Core.Interfaces.Services;
using Authorization.Core.Services;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Authorization.Test.Core.Services;

public class CookieServiceTest
{
    private readonly ICookieService _cookieService;
    private readonly Mock<IResponseCookies> _reponseCookiesMock;
    private readonly Mock<IRequestCookieCollection> _requestCookieCollectionMock;

    public CookieServiceTest()
    {
        _requestCookieCollectionMock = new Mock<IRequestCookieCollection>();
        _reponseCookiesMock = new Mock<IResponseCookies>();

        var accessorMock = new Mock<IHttpContextAccessor>();
        var httpContextMock = new Mock<HttpContext>();
        var httpRequestMock = new Mock<HttpRequest>();
        var httpResponseMock = new Mock<HttpResponse>();

        accessorMock.Setup(x => x.HttpContext).Returns(httpContextMock.Object);
        httpContextMock.Setup(x => x.Request).Returns(httpRequestMock.Object);
        httpContextMock.Setup(x => x.Response).Returns(httpResponseMock.Object);
        httpRequestMock.Setup(x => x.Cookies).Returns(_requestCookieCollectionMock.Object);
        httpResponseMock.Setup(x => x.Cookies).Returns(_reponseCookiesMock.Object);

        _cookieService = new CookieService(accessorMock.Object);
    }

    [Fact]
    public void AddCookie_ShouldAddCookie()
    {
        // Arrange
        var name = "testCookie";
        var value = "testCookieValue";
        var expireDays = 5;

        // Act
        _cookieService.AddCookie(name, value, expireDays);

        // Assert
        _reponseCookiesMock.Verify(x => x.Append(
            It.Is<string>(y => y == name),
            It.Is<string>(y => y == value),
            It.Is<CookieOptions>(y =>
                y.HttpOnly == true
                && y.Expires.HasValue
                && y.Expires.Value.Date == DateTime.UtcNow.AddDays(expireDays).Date
                && y.SameSite == SameSiteMode.None
                && y.Secure == true)
            )
        , Times.Once);
    }

    [Fact]
    public void GetCookieValu_CookieDoesNotExist_ReturnStringEmpty()
    {
        // Arrange
        var name = "testCookie";
        var value = (string)null;
        _requestCookieCollectionMock.Setup(x => x.TryGetValue(name, out value)).Returns(false);
        // Act
        var result = _cookieService.GetCookieValue(name);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void GetCookieValue_CookieExists_ReturnValue()
    {
        // Arrange
        var name = "testCookie";
        var value = "testCookieValue";
        _requestCookieCollectionMock.Setup(x => x.TryGetValue(name, out value)).Returns(true);
        // Act
        var result = _cookieService.GetCookieValue(name);

        // Assert
        Assert.NotEmpty(result);
        Assert.Equal(value, result);
    }
}