using FluentValidation;
using Shared.Core.Bases;
using Shop.Domain.Entities;

namespace Shop.Core.Dtos.CategoryTranslation;

public class CategoryTranslationFormDto : BaseTranslationFormDto<CategoryTranslationEntity, CategoryTranslationFormDto>
{
}

public class CategoryTranslationFormValidator : AbstractValidator<CategoryTranslationFormDto>
{
    public CategoryTranslationFormValidator()
    {
    }
}