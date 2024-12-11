using FluentValidation;
using Product.Domain.Entities;
using Shared.Core.Bases;

namespace Product.Core.Dtos.CategoryTranslation;

public class CategoryTranslationFormDto : BaseTranslationFormDto<CategoryTranslationEntity, CategoryTranslationFormDto>
{
}

public class CategoryTranslationFormValidator : AbstractValidator<CategoryTranslationFormDto>
{
    public CategoryTranslationFormValidator()
    {
    }
}