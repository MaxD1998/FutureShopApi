using Shared.Domain.Bases;
using Shared.Domain.Interfaces;
using Shop.Domain.Aggregates.Products;

namespace Shop.Domain.Aggregates.Baskets.Entities;

public class BasketItemEntity : BaseEntity, IUpdate<BasketItemEntity>
{
    public Guid BasketId { get; set; }

    public Guid ProductId { get; set; }

    public int Quantity { get; set; }

    #region Related Data

    public BasketAggregate Basket { get; set; }

    public ProductAggregate Product { get; set; }

    #endregion Related Data

    public void Update(BasketItemEntity entity)
    {
        ProductId = entity.ProductId;
        Quantity = entity.Quantity;
    }
}