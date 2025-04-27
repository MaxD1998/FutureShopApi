using Shared.Domain.Bases;
using Shared.Domain.Interfaces;

namespace Shared.Domain.Extensions;

public static class CollectionExtension
{
    public static void UpdateEntities<TEntity>(this ICollection<TEntity> entities, ICollection<TEntity> updateEntities) where TEntity : IEntity, IUpdate<TEntity>
    {
        foreach (var updateEntity in updateEntities)
        {
            var result = entities.FirstOrDefault(x => x.Id == updateEntity.Id && x.Id != Guid.Empty);
            if (result is null)
            {
                entities.Add(updateEntity);
                continue;
            }

            result.Update(updateEntity);
        }

        foreach (var entity in entities.ToList())
        {
            if (!updateEntities.Any(x => x.Id == entity.Id))
                entities.Remove(entity);
        }
    }

    public static void UpdateEventEntities<TEntity>(this ICollection<TEntity> entities, ICollection<TEntity> updateEntities) where TEntity : BaseExternalEntity, IUpdateEvent<TEntity>
    {
        foreach (var updateEntity in updateEntities)
        {
            var result = entities.FirstOrDefault(x => x.ExternalId == updateEntity.ExternalId);
            if (result is null)
            {
                entities.Add(updateEntity);
                continue;
            }

            result.UpdateEvent(updateEntity);
        }

        foreach (var entity in entities.ToList())
        {
            if (!updateEntities.Any(x => x.Id == entity.Id))
                entities.Remove(entity);
        }
    }
}