using FluentValidation;
using Product.Domain.Entities;
using Shared.Core.Bases;

namespace Product.Core.Dtos.CategoryTranslation;

public class CategoryTranslationFormDto : BaseTranslationFormDto<CategoryTranslationEntity>
{
    public CategoryTranslationFormDto()
    {
    }

    public CategoryTranslationFormDto(CategoryTranslationEntity entity) : base(entity)
    {
    }
}

public class CategoryTranslationFormValidator : AbstractValidator<CategoryTranslationFormDto>
{
    public CategoryTranslationFormValidator()
    {
    }
}