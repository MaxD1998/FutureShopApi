using Shared.Domain.Bases;
using Shared.Domain.Interfaces;

namespace Shop.Domain.Aggregates.Baskets.Entities;

public class BasketItemEntity : BaseEntity, IUpdate<BasketItemEntity>
{
    public BasketItemEntity(Guid productId, int quantity)
    {
        SetProductId(productId);
        SetQuantity(quantity);
    }

    public BasketItemEntity(Guid id, Guid productId, int quantity)
    {
        Id = id;
        SetProductId(productId);
        SetQuantity(quantity);
    }

    private BasketItemEntity()
    {
    }

    public Guid BasketId { get; private set; }

    public Guid ProductId { get; private set; }

    public int Quantity { get; private set; }

    #region MyRegion

    private void SetProductId(Guid productId)
    {
        ValidateRequiredProperty(nameof(ProductId), productId);

        ProductId = productId;
    }

    private void SetQuantity(int quantity)
    {
        ValidateNonNegativeProperty(nameof(Quantity), quantity);

        Quantity = quantity;
    }

    #endregion MyRegion

    #region Methods

    public void Update(BasketItemEntity entity)
    {
        ProductId = entity.ProductId;
        Quantity = entity.Quantity;
    }

    #endregion Methods
}