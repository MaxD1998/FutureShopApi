using Shared.Domain.Bases;
using Shared.Domain.Extensions;

namespace Product.Domain.Entities;

public class ProductEntity : BaseEntity
{
    public string Description { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public Guid ProductBaseId { get; set; }

    #region Related Data

    public ProductBaseEntity ProductBase { get; set; }

    public ICollection<ProductParameterValueEntity> ProductParameterValues { get; set; } = [];

    public ICollection<ProductPhotoEntity> ProductPhotos { get; set; } = [];

    public ICollection<ProductTranslationEntity> Translations { get; set; } = [];

    #endregion Related Data

    public void Update(ProductEntity entity)
    {
        Description = entity.Description;
        Name = entity.Name;
        Price = entity.Price;
        ProductPhotos.UpdateEntities(entity.ProductPhotos);
        ProductParameterValues.UpdateEntities(entity.ProductParameterValues);
        Translations.UpdateEntities(entity.Translations);
    }
}