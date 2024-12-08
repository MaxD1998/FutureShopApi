using Shared.Domain.Bases;
using Shared.Domain.Interfaces;

namespace Product.Domain.Entities;

public class BasketItemEntity : BaseEntity, IUpdate<BasketItemEntity>
{
    public Guid BasketId { get; set; }

    public Guid ProductId { get; set; }

    public int Quantity { get; set; }

    #region Related Data

    public BasketEntity Basket { get; set; }

    public ProductEntity Product { get; set; }

    #endregion Related Data

    public void Update(BasketItemEntity entity)
    {
        ProductId = entity.ProductId;
        Quantity = entity.Quantity;
    }
}