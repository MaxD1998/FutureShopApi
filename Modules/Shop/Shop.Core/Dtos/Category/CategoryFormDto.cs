using Shop.Core.Dtos.CategoryTranslation;
using Shop.Domain.Aggregates.Categories;
using System.Linq.Expressions;

namespace Shop.Core.Dtos.Category;

public class CategoryFormDto
{
    public bool IsActive { get; set; }

    public string Name { get; set; }

    public Guid? ParentCategoryId { get; set; }

    public List<IdNameDto> SubCategories { get; set; } = [];

    public List<CategoryTranslationFormDto> Translations { get; set; } = [];

    public static Expression<Func<CategoryAggregate, CategoryFormDto>> Map() => entity => new CategoryFormDto
    {
        IsActive = entity.IsActive,
        Name = entity.Name,
        ParentCategoryId = entity.ParentCategoryId,
        SubCategories = entity.SubCategories.AsQueryable().Select(IdNameDto.MapFromCategory()).ToList(),
        Translations = entity.Translations.AsQueryable().Select(CategoryTranslationFormDto.Map()).ToList(),
    };

    public CategoryAggregate ToEntity() => new()
    {
        IsActive = IsActive,
        Translations = Translations.Select(x => x.ToEntity()).ToList(),
    };
}