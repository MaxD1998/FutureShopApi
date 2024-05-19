using FluentValidation;
using Shared.Core.Errors;
using Shared.Core.Extensions;

namespace Product.Core.Dtos.Category;

public class CategoryFormValidator : AbstractValidator<CategoryFormDto>
{
    public CategoryFormValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
                .ErrorResponse(ErrorMessage.ValueWasEmpty);
    }
}