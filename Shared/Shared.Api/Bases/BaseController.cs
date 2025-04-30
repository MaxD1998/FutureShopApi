using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Api.Results;
using Shared.Core.Dtos;
using Shared.Core.Factories.FluentValidator;
using Shared.Domain.Bases;
using Shared.Infrastructure.Dtos;
using System.Net;

namespace Shared.Api.Bases;

[ApiController]
public abstract class BaseController : ControllerBase
{
    private readonly IFluentValidatorFactory _fluentValidatorFactory;

    private readonly IMediator _mediator;

    public BaseController(
    IFluentValidatorFactory fluentValidatorFactory,
    IMediator mediator)
    {
        _fluentValidatorFactory = fluentValidatorFactory;
        _mediator = mediator;
    }

    protected async Task<IActionResult> ApiFileResponseAsync<TFile>(IRequest<ResultDto<TFile>> request, CancellationToken cancellationToken = default) where TFile : BaseFileDocument
    {
        cancellationToken.ThrowIfCancellationRequested();

        var fileResult = await _mediator.Send(request, cancellationToken);
        var file = fileResult.Result;

        return file != null
            ? File(file.Data, file.ContentType, file.Name)
            : NoContent();
    }

    protected async Task<IActionResult> ApiResponseAsync<TRespone>(Func<CancellationToken, Task<ResultDto<TRespone>>> executeAsync, CancellationToken cancellationToken)
        => ApiResponse(await executeAsync(cancellationToken));

    protected async Task<IActionResult> ApiResponseAsync<TParam, TRespone>(Func<TParam, CancellationToken, Task<ResultDto<TRespone>>> executeAsync, TParam param, CancellationToken cancellationToken)
        => ApiResponse(await executeAsync(param, cancellationToken));

    protected async Task<IActionResult> ApiResponseAsync<T1, T2, TRespone>(Func<T1, T2, CancellationToken, Task<ResultDto<TRespone>>> executeAsync, T1 param1, T2 param2, CancellationToken cancellationToken)
        => ApiResponse(await executeAsync(param1, param2, cancellationToken));

    protected async Task<IActionResult> ApiResponseAsync<T1, T2, T3, TRespone>(Func<T1, T2, T3, CancellationToken, Task<ResultDto<TRespone>>> executeAsync, T1 param1, T2 param2, T3 param3, CancellationToken cancellationToken)
        => ApiResponse(await executeAsync(param1, param2, param3, cancellationToken));

    protected async Task<IActionResult> ApiResponseAsync(Func<CancellationToken, Task<ResultDto>> executeAsync, CancellationToken cancellationToken)
        => ApiResponse(await executeAsync(cancellationToken));

    protected async Task<IActionResult> ApiResponseAsync<TParam>(Func<TParam, CancellationToken, Task<ResultDto>> executeAsync, TParam param, CancellationToken cancellationToken)
        => ApiResponse(await executeAsync(param, cancellationToken));

    private IActionResult ApiResponse(ResultDto result)
        => result is ResultDto<ErrorDto> error ? ApiResponse(error) : NoContent();

    private IActionResult ApiResponse<TResult>(ResultDto<TResult> result)
    {
        if (!result.IsSuccess)
            return result.HttpCode switch
            {
                HttpStatusCode.Conflict => Conflict(result.Result),
                HttpStatusCode.Forbidden => new ForbiddenObjectResult(result.Result),
                HttpStatusCode.NotFound => NotFound(result.Result),
                HttpStatusCode.NotImplemented => new NotImplementedObjectResult(result.Result),
                HttpStatusCode.Unauthorized => Unauthorized(result.Result),
                _ => BadRequest(result.Result)
            };

        return Ok(result.Result);
    }
}