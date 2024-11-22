using FluentValidation;

namespace Product.Core.Dtos.ProductParameter;

public class ProductParameterFormValidator : AbstractValidator<ProductParameterFormDto>
{
    public ProductParameterFormValidator()
    {
    }
}