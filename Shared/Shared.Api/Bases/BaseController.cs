using Microsoft.AspNetCore.Mvc;
using Shared.Api.Results;
using Shared.Core.Dtos;
using Shared.Core.Interfaces;
using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Dtos;
using System.Net;

namespace Shared.Api.Bases;

[ApiController]
public abstract class BaseController : ControllerBase
{
    protected async Task<IActionResult> ApiFileResponseAsync<TParam, TFile>(Func<TParam, CancellationToken, Task<ResultDto<TFile>>> executeAsync, TParam param, CancellationToken cancellationToken = default) where TFile : BaseFileDocument
    {
        cancellationToken.ThrowIfCancellationRequested();

        var fileResult = await executeAsync(param, cancellationToken);
        var file = fileResult.Result;

        return file != null
            ? File(file.Data, file.ContentType, file.Name)
            : NoContent();
    }

    protected async Task<IActionResult> ApiResponseAsync(Func<CancellationToken, Task<ResultDto>> executeAsync, CancellationToken cancellationToken)
        => ApiResponse(await executeAsync(cancellationToken));

    protected async Task<IActionResult> ApiResponseAsync<T1, T2>(Func<T1, T2, CancellationToken, Task<ResultDto>> executeAsync, T1 param1, T2 param2, CancellationToken cancellationToken)
    {
        if (param1 is IValidator validator1)
            if (!validator1.Validate(out var result))
                return ApiResponse(result);

        if (param2 is IValidator validator2)
            if (!validator2.Validate(out var result))
                return ApiResponse(result);

        return ApiResponse(await executeAsync(param1, param2, cancellationToken));
    }

    protected async Task<IActionResult> ApiResponseAsync<TRespone>(Func<CancellationToken, Task<ResultDto<TRespone>>> executeAsync, CancellationToken cancellationToken)
        => ApiResponse(await executeAsync(cancellationToken));

    protected async Task<IActionResult> ApiResponseAsync<TParam>(Func<TParam, CancellationToken, Task<ResultDto>> executeAsync, TParam param, CancellationToken cancellationToken)
    {
        if (param is IValidator validator)
            if (!validator.Validate(out var result))
                return ApiResponse(result);

        return ApiResponse(await executeAsync(param, cancellationToken));
    }

    protected async Task<IActionResult> ApiResponseAsync<TParam, TRespone>(Func<TParam, CancellationToken, Task<ResultDto<TRespone>>> executeAsync, TParam param, CancellationToken cancellationToken)
    {
        if (param is IValidator validator)
            if (!validator.Validate(out var result))
                return ApiResponse(result);

        return ApiResponse(await executeAsync(param, cancellationToken));
    }

    protected async Task<IActionResult> ApiResponseAsync<T1, T2, TRespone>(Func<T1, T2, CancellationToken, Task<ResultDto<TRespone>>> executeAsync, T1 param1, T2 param2, CancellationToken cancellationToken)
    {
        if (param1 is IValidator validator1)
            if (!validator1.Validate(out var result))
                return ApiResponse(result);

        if (param2 is IValidator validator2)
            if (!validator2.Validate(out var result))
                return ApiResponse(result);

        return ApiResponse(await executeAsync(param1, param2, cancellationToken));
    }

    protected async Task<IActionResult> ApiResponseAsync<T1, T2, T3, TRespone>(Func<T1, T2, T3, CancellationToken, Task<ResultDto<TRespone>>> executeAsync, T1 param1, T2 param2, T3 param3, CancellationToken cancellationToken)
    {
        if (param1 is IValidator validator1)
            if (!validator1.Validate(out var result))
                return ApiResponse(result);

        if (param2 is IValidator validator2)
            if (!validator2.Validate(out var result))
                return ApiResponse(result);

        if (param3 is IValidator validator3)
            if (!validator3.Validate(out var result))
                return ApiResponse(result);

        return ApiResponse(await executeAsync(param1, param2, param3, cancellationToken));
    }

    private IActionResult ApiResponse(ResultDto result)
        => result is ResultDto<ErrorDto> error ? ApiResponse(error) : NoContent();

    private IActionResult ApiResponse<TResult>(ResultDto<TResult> result)
    {
        if (!result.IsSuccess)
            return result.HttpCode switch
            {
                HttpStatusCode.Conflict => Conflict(result.ErrorMessage),
                HttpStatusCode.Forbidden => new ForbiddenObjectResult(result.ErrorMessage),
                HttpStatusCode.NotFound => NotFound(result.ErrorMessage),
                HttpStatusCode.NotImplemented => new NotImplementedObjectResult(result.ErrorMessage),
                HttpStatusCode.Unauthorized => Unauthorized(result.ErrorMessage),
                _ => BadRequest(result.Result)
            };

        return Ok(result.Result);
    }
}