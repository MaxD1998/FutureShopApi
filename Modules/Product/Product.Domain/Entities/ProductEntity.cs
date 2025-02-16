using Shared.Domain.Bases;
using Shared.Domain.Extensions;

namespace Product.Domain.Entities;

public class ProductEntity : BaseEntity
{
    public string Name { get; set; }

    public Guid ProductBaseId { get; set; }

    #region Related Data

    public ProductBaseEntity ProductBase { get; set; }

    public ICollection<ProductPhotoEntity> ProductPhotos { get; set; } = [];

    #endregion Related Data

    public void Update(ProductEntity entity)
    {
        Name = entity.Name;
        ProductPhotos.UpdateEntities(entity.ProductPhotos);
    }
}