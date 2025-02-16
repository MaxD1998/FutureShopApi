using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Api.Results;
using Shared.Core.Dtos;
using Shared.Core.Errors;
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

    protected async Task<IActionResult> ApiResponseAsync<TParam, TResult>(TParam param, IRequest<ResultDto<TResult>> request, CancellationToken cancellationToken = default)
        where TParam : class
    {
        cancellationToken.ThrowIfCancellationRequested();
        var validationResult = IsValid(param, out var errors);

        if (validationResult.IsSuccess)
        {
            if (!validationResult.Result)
                return BadRequest(errors);
        }
        else
            return ApiResponse(validationResult);

        return ApiResponse(await _mediator.Send(request, cancellationToken));
    }

    protected async Task<IActionResult> ApiResponseAsync<TParam>(TParam param, IRequest<ResultDto> request, CancellationToken cancellationToken = default)
        where TParam : class
    {
        cancellationToken.ThrowIfCancellationRequested();
        var validationResult = IsValid(param, out var errors);

        if (validationResult.IsSuccess)
        {
            if (!validationResult.Result)
                return BadRequest(errors);
        }
        else
            return ApiResponse(validationResult);

        return ApiResponse(await _mediator.Send(request, cancellationToken));
    }

    protected async Task<IActionResult> ApiResponseAsync(IRequest<ResultDto> request, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return ApiResponse(await _mediator.Send(request, cancellationToken));
    }

    protected async Task<IActionResult> ApiResponseAsync<TResult>(IRequest<ResultDto<TResult>> request, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return ApiResponse(await _mediator.Send(request, cancellationToken));
    }

    protected async Task<IActionResult> ApiResponseAsync<TParam, TRespone>(TParam param, Func<Task<ResultDto<TRespone>>> action, CancellationToken cancellationToken = default)
        where TParam : class
    {
        cancellationToken.ThrowIfCancellationRequested();

        var validationResult = IsValid(param, out var errors);

        if (validationResult.IsSuccess)
        {
            if (!validationResult.Result)
                return BadRequest(errors);
        }
        else
            return ApiResponse(validationResult);

        return ApiResponse(await action());
    }

    protected async Task<IActionResult> ApiResponseAsync<T>(Func<Task<ResultDto<T>>> action)
        => ApiResponse(await action());

    protected async Task<IActionResult> ApiResponseAsync(Func<Task<ResultDto>> action)
        => ApiResponse(await action());

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

    private ResultDto<bool> IsValid<TInput>(TInput param, out IEnumerable<ErrorDto> errors) where TInput : class
    {
        var validator = _fluentValidatorFactory.GetValidator<TInput>();

        if (validator is null)
        {
            errors = [];
            return ResultDto.Error<bool>(HttpStatusCode.NotImplemented, CommonExceptionMessage.C002ValidatorNotExist);
        }

        var validation = validator.Validate(param);
        var isValid = validation.IsValid;

        errors = isValid ? null : validation.Errors.Select(x => new ErrorDto(x));

        return ResultDto.Success(isValid);
    }
}