using Shop.Core.Dtos.CategoryTranslation;
using Shop.Infrastructure.Entities;

namespace Shop.Core.Dtos.Category;

public class CategoryRequestFormDto
{
    public bool IsActive { get; set; }

    public string Name { get; set; }

    public Guid? ParentCategoryId { get; set; }

    public List<IdNameDto> SubCategories { get; set; } = [];

    public List<CategoryTranslationFormDto> Translations { get; set; } = [];

    public CategoryEntity ToEntity() => new()
    {
        IsActive = IsActive,
        Translations = Translations.Select(x => x.ToEntity()).ToList(),
    };
}