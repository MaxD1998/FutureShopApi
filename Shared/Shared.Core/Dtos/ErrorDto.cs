namespace Shared.Core.Dtos;

public class ErrorDto
{
    public ErrorDto(string errorMessage)
    {
        ErrorMessage = errorMessage;
    }

    public ErrorDto(string errorCode, string errorMessage)
    {
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
    }

    public string ErrorCode { get; }

    public string ErrorMessage { get; }
}