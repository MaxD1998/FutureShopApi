using Product.Domain.Entities;

namespace Product.Core.Dtos.CategoryTranslation;

public class CategoryTranslationInputDto
{
    public Guid CategoryId { get; set; }

    public string Lang { get; set; }

    public string Translation { get; set; }

    public CategoryTranslationEntity ToEntity() => new()
    {
        CategoryId = CategoryId,
        Lang = Lang,
        Translation = Translation
    };
}