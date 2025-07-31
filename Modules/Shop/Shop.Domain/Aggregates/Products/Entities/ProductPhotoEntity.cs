using Shared.Domain.Bases;
using Shared.Domain.Interfaces;
using Shop.Domain.Aggregates.Products;

namespace Shop.Domain.Aggregates.Products.Entities;

public class ProductPhotoEntity : BaseExternalEntity, IUpdateEvent<ProductPhotoEntity>
{
    public string FileId { get; set; }

    public int Position { get; set; }

    public Guid ProductId { get; set; }

    #region Related Data

    public ProductAggregate Product { get; set; }

    #endregion Related Data

    public void UpdateEvent(ProductPhotoEntity entity)
    {
        Position = entity.Position;
    }
}