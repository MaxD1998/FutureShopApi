using Shop.Core.Dtos.Category.CategoryTranslation;
using Shop.Domain.Entities.Categories;
using System.Linq.Expressions;

namespace Shop.Core.Dtos.Category;

public class CategoryResponseFormDto : CategoryRequestFormDto
{
    public Guid Id { get; set; }

    public static Expression<Func<CategoryEntity, CategoryResponseFormDto>> Map() => entity => new()
    {
        Id = entity.Id,
        IsActive = entity.IsActive,
        Name = entity.Name,
        ParentCategoryId = entity.ParentCategoryId,
        SubCategories = entity.SubCategories.AsQueryable().Select(IdNameDto.MapFromCategory()).ToList(),
        Translations = entity.Translations.AsQueryable().Select(CategoryTranslationFormDto.Map()).ToList(),
    };
}