using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Dtos;
using Shared.Core.Errors;
using Shared.Core.Exceptions;
using Shared.Core.Factories.FluentValidator;

namespace Shared.Api.Bases;

[ApiController]
[Route("[controller]")]
[Authorize]
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

    protected async Task<IActionResult> ApiResponseAsync<TParam>(TParam param, IBaseRequest request)
        where TParam : class
    {
        if (!IsValid(param, out var errors))
            return BadRequest(errors);

        if (request is IRequest)
        {
            await _mediator.Send(request);
            return NoContent();
        }

        return Ok(await _mediator.Send(request));
    }

    protected async Task<IActionResult> ApiResponseAsync<TParam, TRespone>(TParam param, Func<Task<TRespone>> action)
        where TParam : class
    {
        if (!IsValid(param, out var errors))
            return BadRequest(errors);

        return Ok(await action());
    }

    protected async Task<IActionResult> ApiResponseAsync(IBaseRequest request)
    {
        if (request is IRequest)
        {
            await _mediator.Send(request);
            return NoContent();
        }

        return Ok(await _mediator.Send(request));
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
            throw new NoValidatorException(ExceptionMessage.ValidatorNotExist);

        var validation = validator.Validate(param);
        var isValid = validation.IsValid;

        errors = isValid ? null : validation.Errors.Select(x => new ErrorDto(x));

        return isValid;
    }
}