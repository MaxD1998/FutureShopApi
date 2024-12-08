using Microsoft.AspNetCore.Http;

namespace Authorization.Core.Services;

public interface ICookieService
{
    void AddCookie(string name, string value, int expire);

    string GetCookieValue(string name);

    void RemoveCookie(string name);
}

public class CookieService : ICookieService
{
    private readonly IResponseCookies _reponseCookies;
    private readonly IRequestCookieCollection _requestCookieCollection;

    public CookieService(IHttpContextAccessor accessor)
    {
        _reponseCookies = accessor.HttpContext.Response.Cookies;
        _requestCookieCollection = accessor.HttpContext.Request.Cookies;
    }

    public void AddCookie(string name, string value, int expire)
    {
        var cookie = new CookieOptions()
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(expire),
            SameSite = SameSiteMode.None,
            Secure = true,
        };

        _reponseCookies.Append(name, value, cookie);
    }

    public string GetCookieValue(string name)
    {
        var isValue = _requestCookieCollection.TryGetValue(name, out var value);

        return isValue ? value : string.Empty;
    }

    public void RemoveCookie(string name)
    {
        if (!_requestCookieCollection.ContainsKey(name))
            return;

        AddCookie(name, string.Empty, -1);
    }
}