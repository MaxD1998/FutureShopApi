using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Interfaces;

namespace Shared.Infrastructure.Extensions;

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

        var ids = updateEntities.Select(x => x.Id).ToList();
        var toRemove = entities.Where(x => !ids.Contains(x.Id)).ToList();

        foreach (var entity in toRemove)
            entities.Remove(entity);
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

        var ids = updateEntities.Select(x => x.ExternalId).ToList();
        var toRemove = entities.Where(x => !ids.Contains(x.ExternalId)).ToList();

        foreach (var entity in toRemove)
            entities.Remove(entity);
    }
}