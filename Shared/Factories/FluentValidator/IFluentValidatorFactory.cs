using FluentValidation;

namespace Shared.Factories.FluentValidation;

public interface IFluentValidatorFactory
{
    public IValidator<T> GetValidator<T>() where T : class;
}