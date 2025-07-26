using Product.Domain.Aggregates.Products.Entities;
using Shared.Domain.Bases;
using Shared.Domain.Extensions;
using Shared.Domain.Interfaces;

namespace Product.Domain.Aggregates.Products;

public class ProductAggregate : BaseEntity, IUpdate<ProductAggregate>
{
    public string Name { get; set; }

    public Guid ProductBaseId { get; set; }

    #region Related Data

    public ICollection<ProductPhoto> ProductPhotos { get; set; } = [];

    #endregion Related Data

    public void Update(ProductAggregate entity)
    {
        Name = entity.Name;
        ProductPhotos.UpdateEntities(entity.ProductPhotos);
    }
}