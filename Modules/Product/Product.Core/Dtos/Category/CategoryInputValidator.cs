using FluentValidation;
using Shared.Core.Errors;
using Shared.Core.Extensions;

namespace Product.Core.Dtos.Category;

public class CategoryInputValidator : AbstractValidator<CategoryInputDto>
{
    public CategoryInputValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
                .ErrorResponse(ErrorMessage.ValueWasEmpty);
    }
}