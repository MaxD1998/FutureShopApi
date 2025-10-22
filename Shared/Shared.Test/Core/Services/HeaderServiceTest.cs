using Microsoft.AspNetCore.Http;
using Moq;
using Shared.Core.Services;

namespace Shared.Test.Core.Services;

public class HeaderServiceTest
{
    [Fact]
    public void GetHeader_HeaderDoesNotExist_ReturnEmptyString()
    {
        // Arrange
        var headers = new HeaderDictionary();
        var httpContextMock = new Mock<HttpContext>();
        httpContextMock.Setup(c => c.Request.Headers).Returns(headers);

        var accessorMock = new Mock<IHttpContextAccessor>();
        accessorMock.Setup(a => a.HttpContext).Returns(httpContextMock.Object);

        var service = new HeaderService(accessorMock.Object);

        // Act
        var result = service.GetHeader("NonExistentHeader");

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void GetHeader_HeaderExists_ReturnHeaderValue()
    {
        // Arrange
        var headerName = "X-Test-Header";
        var headerValue = "TestValue";
        var headers = new HeaderDictionary { { headerName, headerValue } };

        var httpContextMock = new Mock<HttpContext>();
        httpContextMock.Setup(c => c.Request.Headers).Returns(headers);

        var accessorMock = new Mock<IHttpContextAccessor>();
        accessorMock.Setup(a => a.HttpContext).Returns(httpContextMock.Object);

        var service = new HeaderService(accessorMock.Object);

        // Act
        var result = service.GetHeader(headerName);

        // Assert
        Assert.Equal(headerValue, result);
    }
}