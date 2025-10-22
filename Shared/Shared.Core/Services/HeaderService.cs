using Microsoft.AspNetCore.Http;

namespace Shared.Core.Services;

public interface IHeaderService
{
    string GetHeader(string name);
}

internal class HeaderService(IHttpContextAccessor accessor) : IHeaderService
{
    private readonly IHeaderDictionary _headers = accessor.HttpContext.Request.Headers;

    public string GetHeader(string name)
        => _headers.TryGetValue(name, out var value) ? value : string.Empty;
}