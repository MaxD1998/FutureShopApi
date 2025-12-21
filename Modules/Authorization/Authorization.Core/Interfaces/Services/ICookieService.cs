using Shared.Core.Dtos;

namespace Authorization.Core.Interfaces.Services;

public interface ICookieService
{
    ResultDto AddCookie(string name, string value, int expire);

    ResultDto<string> GetCookieValue(string name);

    ResultDto RemoveCookie(string name);
}
