using Shared.Core.Dtos;

namespace Shared.Core.Errors;

public static class ErrorMessage
{
    #region Common

    public static ErrorMessageDto ValueWasEmpty => new ErrorMessageDto("C001", "Value was empty");

    public static ErrorMessageDto IsNotEmail => new ErrorMessageDto("C002", "Input value is not email");

    public static ErrorMessageDto TooLongString => new ErrorMessageDto("C003", "String was too long");

    #endregion Common
}