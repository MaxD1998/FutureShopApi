using Microsoft.AspNetCore.Http;

namespace Shared.Core.Services;

public interface IHeaderService
{
    string GetHeader(string name);
}

public class HeaderService(IHttpContextAccessor accessor) : IHeaderService
{
    private readonly IHeaderDictionary _headers = accessor.HttpContext.Request.Headers;

    public string GetHeader(string name)
    {
        var isValue = _headers.TryGetValue(name, out var value);

        return isValue ? value : string.Empty;
    }
}