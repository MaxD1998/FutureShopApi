using Shop.Infrastructure.Persistence.Entities.Products;

namespace Shop.Infrastructure.Persistence.DomainLogics;

public static class PriceDomainLogic
{
    public static void Add(ICollection<PriceEntity> entities, PriceEntity updateEntity, DateTime utcNow, bool wasActive)
    {
        if (wasActive)
        {
            if (utcNow <= updateEntity.Start)
            {
                if (!updateEntity.End.HasValue)
                    AddLastPrice(entities, updateEntity);
                else
                    AddPriceBetween(entities, updateEntity);
            }
        }
        else
        {
            if (!updateEntity.Start.HasValue || entities.Count == 0)
                AddFirstPrice(entities, updateEntity);
            else if (!updateEntity.End.HasValue)
                AddLastPrice(entities, updateEntity);
            else
                AddPriceBetween(entities, updateEntity);
        }
    }

    public static void Remove(ICollection<PriceEntity> entities, ICollection<PriceEntity> updateEntities, DateTime utcNow, bool wasActive)
    {
        foreach (var entity in entities.ToList())
        {
            if (wasActive)
            {
                if (entity.Start.HasValue && utcNow < entity.Start)
                {
                    if (!updateEntities.Any(x => x.Id == entity.Id))
                    {
                        var priceBefore = entities.FirstOrDefault(x => x.End == entity.Start);
                        priceBefore.End = entity.End;

                        entities.Remove(entity);
                    }
                }
            }
            else
            {
                if (!updateEntities.Any(x => x.Id == entity.Id))
                {
                    if (!entity.Start.HasValue)
                    {
                        var priceAfter = entities.FirstOrDefault(x => x.Start == entity.End);
                        priceAfter.Start = null;
                    }
                    else
                    {
                        var priceBefore = entities.FirstOrDefault(x => x.End == entity.Start);
                        priceBefore.End = entity.End;
                    }

                    entities.Remove(entity);
                }
            }
        }
    }

    public static void Update(ICollection<PriceEntity> entities, PriceEntity updateEntity, DateTime utcNow, bool wasActive)
    {
        var entityToUpdate = entities.FirstOrDefault(x => x.Id == updateEntity.Id && x.Id != Guid.Empty);

        if (entityToUpdate == null)
            return;

        if (wasActive)
        {
            if (utcNow < updateEntity.Start && utcNow < entityToUpdate.Start)
                Update(entities, updateEntity, entityToUpdate);
        }
        else
        {
            if (!entityToUpdate.Start.HasValue)
                updateEntity.Start = null;

            Update(entities, updateEntity, entityToUpdate);
        }
    }

    private static void AddFirstPrice(ICollection<PriceEntity> entities, PriceEntity updateEntity)
    {
        if (entities.Count != 0)
        {
            var firstPrice = entities.FirstOrDefault(x => !x.Start.HasValue);

            if (!firstPrice.Start.HasValue && updateEntity.End.HasValue)
            {
                firstPrice.Start = updateEntity.End;
                entities.Add(updateEntity);
            }
        }
        else
        {
            updateEntity.Start = null;
            updateEntity.End = null;

            entities.Add(updateEntity);
        }
    }

    private static void AddLastPrice(ICollection<PriceEntity> entities, PriceEntity updateEntity)
    {
        var lastPrice = entities.FirstOrDefault(x => !x.End.HasValue);

        if (!lastPrice.End.HasValue && updateEntity.Start.HasValue)
        {
            lastPrice.End = updateEntity.Start;
            entities.Add(updateEntity);
        }
    }

    private static void AddPriceBetween(ICollection<PriceEntity> entities, PriceEntity updateEntity)
    {
        if (!updateEntity.Start.HasValue || !updateEntity.End.HasValue)
            return;

        var priceBeforeCount = entities.Count(x => updateEntity.Start <= x.End);
        var priceAfterCount = entities.Count(x => x.Start <= updateEntity.End);

        if (priceBeforeCount != 1 && priceAfterCount != 1)
            return;

        var priceBefore = entities.FirstOrDefault(x => updateEntity.Start <= x.End);
        var priceAfter = entities.FirstOrDefault(x => x.Start <= updateEntity.End);

        if (priceBefore != null && priceAfter != null)
        {
            priceBefore.End = updateEntity.Start;
            priceAfter.Start = updateEntity.End;
            entities.Add(updateEntity);
        }
    }

    private static void Update(ICollection<PriceEntity> entities, PriceEntity updateEntity, PriceEntity entityToUpdate)
    {
        if (!entityToUpdate.End.HasValue)
            updateEntity.End = null;

        var priceBefore = entities.FirstOrDefault(x => x.End == entityToUpdate.Start);
        var priceAfter = entities.FirstOrDefault(x => x.Start == entityToUpdate.End);

        if (priceBefore != null && priceBefore.End != updateEntity.Start)
            priceBefore.End = updateEntity.Start;

        if (priceAfter != null && priceBefore.Start != updateEntity.End)
            priceAfter.Start = updateEntity.End;

        entityToUpdate.Update(updateEntity);
    }
}