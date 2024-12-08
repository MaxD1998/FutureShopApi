using System.Net;

namespace Shared.Core.Dtos;

public class ResultDto
{
    public ResultDto(HttpStatusCode httpCode, bool isSuccess)
    {
        HttpCode = httpCode;
        IsSuccess = isSuccess;
    }

    public HttpStatusCode HttpCode { get; init; }

    public bool IsSuccess { get; init; } = false;
}

public class ResultDto<T> : ResultDto
{
    public ResultDto(HttpStatusCode httpCode, bool isSuccess, T result) : base(httpCode, isSuccess)
    {
        Result = result;
    }

    public T Result { get; set; }
}