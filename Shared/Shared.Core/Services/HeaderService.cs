using Microsoft.AspNetCore.Http;
using Shared.Core.Interfaces.Services;

namespace Shared.Core.Services;

internal class HeaderService(IHttpContextAccessor accessor) : IHeaderService
{
    private readonly IHeaderDictionary _headers = accessor.HttpContext.Request.Headers;

    public string GetHeader(string name)
        => _headers.TryGetValue(name, out var value) ? value : string.Empty;
}