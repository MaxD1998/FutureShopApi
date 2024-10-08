using Shared.Domain.Bases;

namespace Shared.Core.Bases;

public abstract class BaseTranslationFormDto<TEntity> where TEntity : BaseTranslationEntity<TEntity>, new()
{
    public BaseTranslationFormDto()
    {
    }

    public BaseTranslationFormDto(TEntity entity)
    {
        Id = entity.Id;
        Lang = entity.Lang;
        Translation = entity.Translation;
    }

    public Guid? Id { get; set; }

    public string Lang { get; set; }

    public string Translation { get; set; }

    public TEntity ToEntity() => new()
    {
        Id = Id ?? Guid.Empty,
        Lang = Lang,
        Translation = Translation
    };
}