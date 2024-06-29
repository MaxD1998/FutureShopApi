using Product.Domain.Entities;

namespace Product.Core.Dtos.ProductParameterTranslation;

public class ProgramParameterTranslationFormDto
{
    public ProgramParameterTranslationFormDto()
    {
    }

    public ProgramParameterTranslationFormDto(ProductParameterTranslationEntity entity)
    {
        Lang = entity.Lang;
        Translation = entity.Translation;
    }

    public string Lang { get; set; }

    public string Translation { get; set; }

    public ProductParameterTranslationEntity ToEntity() => new()
    {
        Lang = Lang,
        Translation = Translation
    };
}