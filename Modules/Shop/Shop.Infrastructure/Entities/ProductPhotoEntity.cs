using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Interfaces;

namespace Shop.Infrastructure.Entities;

public class ProductPhotoEntity : BaseExternalEntity, IUpdateEvent<ProductPhotoEntity>
{
    public string FileId { get; set; }

    public int Position { get; set; }

    public Guid ProductId { get; set; }

    #region Related Data

    public ProductEntity Product { get; set; }

    #endregion Related Data

    public void UpdateEvent(ProductPhotoEntity entity)
    {
        Position = entity.Position;
    }
}