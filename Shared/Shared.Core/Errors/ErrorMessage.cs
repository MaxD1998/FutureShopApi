using Shared.Infrastructure.Dtos;

namespace Shared.Core.Errors;

public static class ErrorMessage
{
    #region Common

    public static ErrorDto IsNotEmail => new("C002", "Input value is not email");

    public static ErrorDto TooLongString => new("C003", "String was too long");

    public static ErrorDto ValueWasEmpty => new("C001", "Value was empty");

    #endregion Common
}