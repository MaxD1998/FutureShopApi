namespace Shared.Core.Dtos;

public class ErrorDto
{
    public ErrorDto()
    {
    }

    public ErrorDto(ErrorMessageDto dto)
    {
        ErrorCode = dto.ErrorCode;
        ErrorMessage = dto.ErrorMessage;
    }

    public string ErrorCode { get; set; }

    public string ErrorMessage { get; set; }

    public string PropertyName { get; set; }
}