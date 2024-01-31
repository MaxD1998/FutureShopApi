using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Core.Factories.FluentValidator;

public class FluentValidatorFactory : IFluentValidatorFactory
{
    private readonly IServiceProvider _service;

    public FluentValidatorFactory(IServiceProvider service)
    {
        _service = service;
    }

    public IValidator<T> GetValidator<T>() where T : class
        => _service.GetService<IValidator<T>>();
}