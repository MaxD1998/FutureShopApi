using Product.Domain.Entities;

namespace Product.Core.Dtos.CategoryTranslation;

public class CategoryTranslationFormDto
{
    public CategoryTranslationFormDto()
    {
    }

    public CategoryTranslationFormDto(CategoryTranslationEntity entity)
    {
        Lang = entity.Lang;
        Translation = entity.Translation;
    }

    public string Lang { get; set; }

    public string Translation { get; set; }

    public CategoryTranslationEntity ToEntity() => new()
    {
        Lang = Lang,
        Translation = Translation
    };
}