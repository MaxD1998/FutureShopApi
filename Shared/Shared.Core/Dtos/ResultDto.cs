using Shared.Infrastructure.Dtos;
using System.Net;

namespace Shared.Core.Dtos;

public class ResultDto
{
    protected ResultDto(HttpStatusCode httpCode, bool isSuccess)
    {
        HttpCode = httpCode;
        IsSuccess = isSuccess;
    }

    public ErrorDto ErrorMessage { get; set; }

    public HttpStatusCode HttpCode { get; init; }

    public bool IsSuccess { get; init; } = false;

    public static ResultDto Error(HttpStatusCode httpCode, ErrorDto dto)
        => new(httpCode, false)
        {
            ErrorMessage = dto
        };

    public static ResultDto<T> Error<T>(HttpStatusCode httpCode, ErrorDto dto)
        => new(httpCode)
        {
            ErrorMessage = dto
        };

    public static ResultDto Success()
        => new(HttpStatusCode.NoContent, true);

    public static ResultDto<T> Success<T>(T result)
        => new(HttpStatusCode.OK, true, result);
}

public class ResultDto<T> : ResultDto
{
    public ResultDto(HttpStatusCode httpCode) : base(httpCode, false)
    {
    }

    public ResultDto(HttpStatusCode httpCode, bool isSuccess, T result) : base(httpCode, isSuccess)
    {
        Result = result;
    }

    public T Result { get; set; }
}