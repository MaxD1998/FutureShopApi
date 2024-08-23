using Shared.Infrastructure.Dtos;

namespace Shared.Core.Errors;

public static class ErrorMessage
{
    #region Common

    public static ErrorMessageDto IsNotEmail => new("C002", "Input value is not email");

    public static ErrorMessageDto TooLongString => new("C003", "String was too long");

    public static ErrorMessageDto ValueWasEmpty => new("C001", "Value was empty");

    #endregion Common
}