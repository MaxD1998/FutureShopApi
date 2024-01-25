namespace Shared.Dtos;

public class ErrorMessageDto
{
    public ErrorMessageDto(string errorCode, string errorMessage)
    {
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
    }

    public string ErrorCode { get; }

    public string ErrorMessage { get; }
}