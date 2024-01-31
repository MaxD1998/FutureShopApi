using FluentValidation;

namespace Shared.Core.Factories.FluentValidator;

public interface IFluentValidatorFactory
{
    public IValidator<T> GetValidator<T>() where T : class;
}