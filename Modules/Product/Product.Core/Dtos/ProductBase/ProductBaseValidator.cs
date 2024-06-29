using FluentValidation;
using Shared.Core.Errors;
using Shared.Core.Extensions;

namespace Product.Core.Dtos.ProductBase;

public class ProductBaseValidator : AbstractValidator<ProductBaseFormDto>
{
    public ProductBaseValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
                .ErrorResponse(ErrorMessage.ValueWasEmpty);
    }
}