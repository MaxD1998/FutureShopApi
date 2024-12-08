using FluentValidation;
using Product.Core.Dtos.CategoryTranslation;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Errors;
using Shared.Core.Extensions;

namespace Product.Core.Dtos.Category;

public class CategoryFormDto
{
    public CategoryFormDto()
    {
    }

    public CategoryFormDto(CategoryEntity entity)
    {
        Name = entity.Name;
        ParentCategoryId = entity.ParentCategoryId;
        SubCategories = entity.SubCategories.Select(x => new IdNameDto(x)).ToList();
        Translations = entity.Translations.Select(x => new CategoryTranslationFormDto(x)).ToList();
    }

    public string Name { get; set; }

    public Guid? ParentCategoryId { get; set; }

    public List<IdNameDto> SubCategories { get; set; } = [];

    public List<CategoryTranslationFormDto> Translations { get; set; } = [];

    public CategoryEntity ToEntity(ProductPostgreSqlContext context) => new()
    {
        Name = Name,
        ParentCategoryId = ParentCategoryId,
        SubCategories = SubCategories != null ? context.Set<CategoryEntity>().Where(x => SubCategories.Select(x => x.Id).Contains(x.Id)).ToList() : null,
        Translations = Translations.Select(x => x.ToEntity()).ToList(),
    };
}

public class CategoryFormValidator : AbstractValidator<CategoryFormDto>
{
    public CategoryFormValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
                .ErrorResponse(ErrorMessage.ValueWasEmpty);
    }
}