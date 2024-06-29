using FluentValidation;

namespace Product.Core.Dtos.ProductParameter;

public class ProductParameterValidator : AbstractValidator<ProductParameterFormDto>
{
    public ProductParameterValidator()
    {
    }
}