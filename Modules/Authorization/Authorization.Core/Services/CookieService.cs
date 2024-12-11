using Microsoft.AspNetCore.Http;
using Shared.Core.Bases;
using Shared.Core.Dtos;

namespace Authorization.Core.Services;

public interface ICookieService
{
    ResultDto AddCookie(string name, string value, int expire);

    ResultDto<string> GetCookieValue(string name);

    ResultDto RemoveCookie(string name);
}

public class CookieService : BaseService, ICookieService
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

        return Success();
    }

    public ResultDto<string> GetCookieValue(string name)
    {
        var isValue = _requestCookieCollection.TryGetValue(name, out var value);

        return Success(isValue ? value : string.Empty);
    }

    public ResultDto RemoveCookie(string name)
    {
        if (!_requestCookieCollection.ContainsKey(name))
            return Success();

        AddCookie(name, string.Empty, -1);

        return Success();
    }
}