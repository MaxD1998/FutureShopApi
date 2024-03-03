using FluentValidation;

namespace Product.Core.Dtos.CategoryTranslation;

public class CategoryTranslationValidator : AbstractValidator<CategoryTranslationInputDto>
{
    public CategoryTranslationValidator()
    {
    }
}