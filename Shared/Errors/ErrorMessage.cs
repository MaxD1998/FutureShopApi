using Shared.Dtos;

namespace Shared.Errors;

public static class ErrorMessage
{
    #region Common

    public static ErrorMessageDto ValueWasEmpty => new ErrorMessageDto("C001", "Value was empty");

    public static ErrorMessageDto IsNotEmail => new ErrorMessageDto("C002", "Input value is not email");

    #endregion Common
}