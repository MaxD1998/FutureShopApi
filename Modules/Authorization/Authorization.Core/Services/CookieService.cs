using Microsoft.AspNetCore.Http;
using Shared.Core.Dtos;
using Authorization.Core.Interfaces.Services;

namespace Authorization.Core.Services;

internal class CookieService : ICookieService
{
    private readonly IResponseCookies _reponseCookies;
    private readonly IRequestCookieCollection _requestCookieCollection;

    public CookieService(IHttpContextAccessor accessor)
    {
        _reponseCookies = accessor.HttpContext.Response.Cookies;
        _requestCookieCollection = accessor.HttpContext.Request.Cookies;
    }

    public ResultDto AddCookie(string name, string value, int expire)
    {
        var cookie = new CookieOptions()
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(expire),
            SameSite = SameSiteMode.None,
            Secure = true,
        };

        _reponseCookies.Append(name, value, cookie);

        return ResultDto.Success();
    }

    public ResultDto<string> GetCookieValue(string name)
    {
        var isValue = _requestCookieCollection.TryGetValue(name, out var value);

        return ResultDto.Success(isValue ? value : string.Empty);
    }

    public ResultDto RemoveCookie(string name)
    {
        if (!_requestCookieCollection.ContainsKey(name))
            return ResultDto.Success();

        AddCookie(name, string.Empty, -1);

        return ResultDto.Success();
    }
}