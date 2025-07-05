using Shared.Domain.Bases;
using Shared.Domain.Extensions;
using Shared.Domain.Interfaces;
using Shop.Domain.Logics;

namespace Shop.Domain.Entities;

public class ProductEntity : BaseExternalEntity, IUpdate<ProductEntity>, IUpdateEvent<ProductEntity>
{
    public bool IsActive { get; set; }

    public string Name { get; set; }

    public Guid ProductBaseId { get; set; }

    public bool WasActive { get; set; }

    #region Related Data

    public ICollection<BasketItemEntity> BasketItems { get; set; } = [];

    public ICollection<PriceEntity> Prices { get; set; } = [];

    public ProductBaseEntity ProductBase { get; set; }

    public ICollection<ProductParameterValueEntity> ProductParameterValues { get; set; } = [];

    public ICollection<ProductPhotoEntity> ProductPhotos { get; set; } = [];

    public ICollection<PurchaseListItemEntity> PurchaseListItems { get; set; } = [];

    public ICollection<ProductTranslationEntity> Translations { get; set; } = [];

    #endregion Related Data

    public void Update(ProductEntity entity)
    {
        IsActive = entity.IsActive;
        Name = entity.Name;

        if (!WasActive && entity.IsActive)
            WasActive = entity.IsActive;

        ProductParameterValues.UpdateEntities(entity.ProductParameterValues);
        Translations.UpdateEntities(entity.Translations);

        UpdatePrices(Prices, entity.Prices);
    }

    public void UpdateEvent(ProductEntity entity)
    {
        Name = entity.Name;
        ProductPhotos.UpdateEventEntities(entity.ProductPhotos);
    }

    private void UpdatePrices(ICollection<PriceEntity> entities, ICollection<PriceEntity> updateEntities)
    {
        var utcNow = DateTime.UtcNow;

        foreach (var updateEntity in updateEntities)
        {
            if (entities.Count == 0)
            {
                updateEntity.Start = null;
                updateEntity.End = null;

                entities.Add(updateEntity);
                continue;
            }

            var isExist = entities.Any(x => x.Id == updateEntity.Id && x.Id != Guid.Empty);

            if (!isExist)
            {
                ProductLogic.Add(entities, updateEntity, utcNow, WasActive);
                continue;
            }

            ProductLogic.Update(entities, updateEntity, utcNow, WasActive);
        }

        ProductLogic.Remove(entities, updateEntities, utcNow, WasActive);
    }
}