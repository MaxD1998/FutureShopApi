namespace Core.Interfaces.Services;

public interface ICookieService
{
    void AddCookie(string name, string value, int expire, bool httpOnly);

    string GetCookie(string name);

    void RemoveCookie(string name);
}