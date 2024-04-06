using Product.Core.Dtos.CategoryTranslation;
using Product.Domain.Entities;
using Shared.Core.Interfaces;

namespace Product.Core.Dtos.Category;

public class CategoryInputDto : IInputDto
{
    public string Name { get; set; }

    public Guid? ParentCategoryId { get; set; }

    public List<CategoryTranslationInputDto> Translations { get; set; }

    public CategoryEntity ToEntity() => new CategoryEntity()
    {
        Name = Name,
        ParentCategoryId = ParentCategoryId,
        Translations = Translations.Select(x => x.ToEntity()).ToList(),
    };
}