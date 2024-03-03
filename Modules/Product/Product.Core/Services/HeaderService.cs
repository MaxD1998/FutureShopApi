using Microsoft.AspNetCore.Http;
using Product.Core.Interfaces.Services;

namespace Product.Core.Services;

public class HeaderService : IHeaderService
{
    private readonly IHeaderDictionary _headers;

    public HeaderService(IHttpContextAccessor accessor)
    {
        _headers = accessor.HttpContext.Request.Headers;
    }

    public string GetHeader(string name)
    {
        var isValue = _headers.TryGetValue(name, out var value);

        return isValue ? value : string.Empty;
    }
}