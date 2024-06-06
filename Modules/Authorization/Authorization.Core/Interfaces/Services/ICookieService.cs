namespace Authorization.Core.Interfaces.Services;

public interface ICookieService
{
    void AddCookie(string name, string value, int expire);

    string GetCookieValue(string name);

    void RemoveCookie(string name);
}