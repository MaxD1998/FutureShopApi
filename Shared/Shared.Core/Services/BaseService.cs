using Shared.Core.Dtos;
using Shared.Infrastructure.Dtos;
using System.Net;

namespace Shared.Core.Services;

public abstract class BaseService
{
    protected ResultDto<ErrorDto> Error(HttpStatusCode httpCode, ErrorMessageDto dto)
        => new ResultDto<ErrorDto>(httpCode, false, new ErrorDto(dto));

    protected ResultDto Success()
        => new ResultDto(HttpStatusCode.NoContent, true);

    protected ResultDto<T> Success<T>(T result)
        => new ResultDto<T>(HttpStatusCode.OK, true, result);
}