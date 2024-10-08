using Shared.Domain.Interfaces;

namespace Shared.Domain.Bases;

public abstract class BaseTranslationEntity<TEntity> : BaseEntity, IUpdate<TEntity> where TEntity : BaseTranslationEntity<TEntity>
{
    public string Lang { get; set; }

    public string Translation { get; set; }

    public void Update(TEntity entity)
    {
        Translation = entity.Translation;
    }
}