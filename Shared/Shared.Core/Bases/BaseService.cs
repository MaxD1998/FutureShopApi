using Shared.Core.Dtos;
using Shared.Infrastructure.Dtos;
using System.Net;

namespace Shared.Core.Bases;

public abstract class BaseService
{
    protected ResultDto<T> Error<T>(HttpStatusCode httpCode, ErrorDto dto)
        => ResultDto.Error<T>(httpCode, dto);

    protected ResultDto Error(HttpStatusCode httpCode, ErrorDto dto)
        => ResultDto.Error(httpCode, dto);

    protected ResultDto Success()
        => ResultDto.Success();

    protected ResultDto<T> Success<T>(T result)
        => ResultDto.Success(result);
}