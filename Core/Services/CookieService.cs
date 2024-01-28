using Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace Core.Services;

public class CookieService : ICookieService
{
    private readonly IHttpContextAccessor _accessor;

    public CookieService(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    public void AddCookie(string name, string value, int expire, bool httpOnly)
    {
        var cookie = new CookieOptions()
        {
            HttpOnly = httpOnly,
            Expires = DateTime.UtcNow.AddDays(expire),
            SameSite = SameSiteMode.None,
            Secure = true,
        };

        _accessor.HttpContext.Response.Cookies.Append(name, value, cookie);
    }

    public string GetCookie(string name)
    {
        var isValue = _accessor.HttpContext.Request.Cookies.TryGetValue(name, out var value);

        return isValue ? value : string.Empty;
    }

    public void RemoveCookie(string name)
        => _accessor.HttpContext.Response.Cookies.Delete(name);
}