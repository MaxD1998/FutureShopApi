using FluentValidation;
using Shared.Core.Errors;
using Shared.Core.Extensions;
using Shop.Core.Dtos;
using Shop.Core.Dtos.CategoryTranslation;
using Shop.Domain.Entities;
using Shop.Infrastructure;
using System.Linq.Expressions;

namespace Shop.Core.Dtos.Category;

public class CategoryFormDto
{
    public string Name { get; set; }

    public Guid? ParentCategoryId { get; set; }

    public List<IdNameDto> SubCategories { get; set; } = [];

    public List<CategoryTranslationFormDto> Translations { get; set; } = [];

    public static Expression<Func<CategoryEntity, CategoryFormDto>> Map() => entity => new CategoryFormDto
    {
        Name = entity.Name,
        ParentCategoryId = entity.ParentCategoryId,
        SubCategories = entity.SubCategories.AsQueryable().Select(IdNameDto.MapFromCategory()).ToList(),
        Translations = entity.Translations.AsQueryable().Select(CategoryTranslationFormDto.Map()).ToList(),
    };

    public CategoryEntity ToEntity(ShopContext context) => new()
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