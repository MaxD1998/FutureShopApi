using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Errors;
using Shared.Core.Exceptions;
using Shared.Core.Factories.FluentValidator;
using Shared.Domain.Bases;
using Shared.Infrastructure.Dtos;

namespace Shared.Api.Bases;

[ApiController]
[Route("[controller]")]
public class BaseController : ControllerBase
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

    protected async Task<IActionResult> ApiFileResponseAsync<TFile>(IRequest<TFile> request, CancellationToken cancellationToken = default) where TFile : BaseFileDocument
    {
        cancellationToken.ThrowIfCancellationRequested();

        var file = await _mediator.Send(request);

        return file != null
            ? File(file.Data, file.ContentType, file.Name)
            : NoContent();
    }

    protected async Task<IActionResult> ApiResponseAsync<TParam>(TParam param, IBaseRequest request, CancellationToken cancellationToken = default)
        where TParam : class
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (!IsValid(param, out var errors))
            return BadRequest(errors);

        if (request is IRequest)
        {
            await _mediator.Send(request, cancellationToken);
            return NoContent();
        }

        return Ok(await _mediator.Send(request, cancellationToken));
    }

    protected async Task<IActionResult> ApiResponseAsync<TParam, TRespone>(TParam param, Func<Task<TRespone>> action, CancellationToken cancellationToken = default)
        where TParam : class
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (!IsValid(param, out var errors))
            return BadRequest(errors);

        return Ok(await action());
    }

    protected async Task<IActionResult> ApiResponseAsync(IBaseRequest request, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (request is IRequest)
        {
            await _mediator.Send(request, cancellationToken);
            return NoContent();
        }

        return Ok(await _mediator.Send(request, cancellationToken));
    }

    protected async Task<IActionResult> ApiResponseAsync<T>(Func<Task<T>> action)
        => Ok(await action());

    protected async Task<IActionResult> ApiResponseAsync(Func<Task> action)
    {
        await action();

        return NoContent();
    }

    protected bool IsValid<TInput>(TInput param, out IEnumerable<ErrorDto> errors) where TInput : class
    {
        var validator = _fluentValidatorFactory.GetValidator<TInput>();

        if (validator is null)
            throw new NoValidatorException(CommonExceptionMessage.C002ValidatorNotExist);

        var validation = validator.Validate(param);
        var isValid = validation.IsValid;

        errors = isValid ? null : validation.Errors.Select(x => new ErrorDto(x));

        return isValid;
    }
}