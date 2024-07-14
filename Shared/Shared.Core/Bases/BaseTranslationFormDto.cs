using Shared.Domain.Bases;

namespace Shared.Core.Bases;

public abstract class BaseTranslationFormDto<TEntity> where TEntity : BaseTranslationEntity, new()
{
    public BaseTranslationFormDto()
    {
    }

    public BaseTranslationFormDto(TEntity entity)
    {
        Lang = entity.Lang;
        Translation = entity.Translation;
    }

    public string Lang { get; set; }

    public string Translation { get; set; }

    public TEntity ToEntity() => new()
    {
        Lang = Lang,
        Translation = Translation
    };
}