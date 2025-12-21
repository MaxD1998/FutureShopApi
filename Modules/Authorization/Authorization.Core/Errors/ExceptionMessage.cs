using Shared.Core.Dtos;

namespace Authorization.Core.Errors;

public static class ExceptionMessage
{
    public static ErrorDto User001WrongEmailOrPassword => new("User001", "Wrong email or password");

    public static ErrorDto User002WrongRefreshTokenFormat => new("User002", "Wrong refresh token format");

    public static ErrorDto User003OldPasswordWasWrong => new("User003", "Old password was wrong");
}