using Shared.Domain.Bases;
using Shared.Domain.Interfaces;

namespace Product.Domain.Entities;

public class ProductPhotoEntity : BaseEntity, IUpdate<ProductPhotoEntity>
{
    public string FileId { get; set; }

    public int Position { get; set; }

    public Guid ProductId { get; set; }

    #region Related Data

    public ProductEntity Product { get; set; }

    #endregion Related Data

    public void Update(ProductPhotoEntity entity)
    {
        Position = entity.Position;
    }
}