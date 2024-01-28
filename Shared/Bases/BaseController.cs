using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using Shared.Errors;
using Shared.Exceptions;
using Shared.Factories.FluentValidation;

namespace Shared.Bases;

[ApiController]
[Route("[controller]")]
public class BaseController : ControllerBase
{
    private readonly IFluentValidatorFactory _fluentValidatorFactory;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public BaseController(
    IFluentValidatorFactory fluentValidatorFactory,
    IMapper mapper,
    IMediator mediator)
    {
        _fluentValidatorFactory = fluentValidatorFactory;
        _mapper = mapper;
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

    protected async Task<IActionResult> ApiResponseAsync<TRespone>(IBaseRequest request)
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

        errors = isValid ? null : _mapper.Map<IEnumerable<ErrorDto>>(validation.Errors);

        return isValid;
    }
}