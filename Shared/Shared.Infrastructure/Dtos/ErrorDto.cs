using FluentValidation.Results;

namespace Shared.Infrastructure.Dtos;

public class ErrorDto
{
    public ErrorDto()
    {
    }

    public ErrorDto(ValidationFailure dto)
    {
        ErrorCode = dto.ErrorCode;
        ErrorMessage = dto.ErrorMessage;
        PropertyName = dto.PropertyName;
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