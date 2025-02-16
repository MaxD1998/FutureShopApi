using Shared.Domain.Bases;
using Shared.Domain.Extensions;
using Shared.Domain.Interfaces;

namespace Shop.Domain.Entities;

public class ProductEntity : BaseExternalEntity, IUpdate<ProductEntity>, IUpdateEvent<ProductEntity>
{
    public string Name { get; set; }

    public decimal Price { get; set; }

    public Guid ProductBaseId { get; set; }

    #region Related Data

    public ICollection<BasketItemEntity> BasketItems { get; set; } = [];

    public ProductBaseEntity ProductBase { get; set; }

    public ICollection<ProductParameterValueEntity> ProductParameterValues { get; set; } = [];

    public ICollection<ProductPhotoEntity> ProductPhotos { get; set; } = [];

    public ICollection<PurchaseListItemEntity> PurchaseListItems { get; set; } = [];

    public ICollection<ProductTranslationEntity> Translations { get; set; } = [];

    #endregion Related Data

    public void Update(ProductEntity entity)
    {
        Name = entity.Name;
        Price = entity.Price;
        ProductParameterValues.UpdateEntities(entity.ProductParameterValues);
        Translations.UpdateEntities(entity.Translations);
    }

    public void UpdateEvent(ProductEntity entity)
    {
        Name = entity.Name;
        ProductPhotos.UpdateEventEntities(entity.ProductPhotos);
    }
}