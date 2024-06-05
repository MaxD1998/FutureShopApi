using Product.Core.Dtos.CategoryTranslation;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Interfaces;

namespace Product.Core.Dtos.Category;

public class CategoryFormDto : IFormDto
{
    public CategoryFormDto()
    {
    }

    public CategoryFormDto(CategoryEntity entity)
    {
        Name = entity.Name;
        ParentCategoryId = entity.ParentCategoryId;
        SubCategoryIds = entity.SubCategories.Select(x => x.Id).ToList();
        Translations = entity.Translations.Select(x => new CategoryTranslationFormDto(x)).ToList();
    }

    public string Name { get; set; }

    public Guid? ParentCategoryId { get; set; }

    public List<Guid> SubCategoryIds { get; set; }

    public List<CategoryTranslationFormDto> Translations { get; set; }

    public CategoryEntity ToEntity(ProductContext context) => new()
    {
        Name = Name,
        ParentCategoryId = ParentCategoryId,
        SubCategories = context.Set<CategoryEntity>().Where(x => SubCategoryIds.Contains(x.Id)).ToList(),
        Translations = Translations.Select(x => x.ToEntity()).ToList(),
    };
}