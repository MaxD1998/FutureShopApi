using Shared.Domain.Bases;
using Shared.Domain.Exceptions;
using Shared.Domain.Interfaces;
using Shop.Infrastructure.Persistence.Entities.Products;

namespace Shop.Infrastructure.Persistence.Entities.Baskets;

public class BasketItemEntity : BaseEntity, IUpdate<BasketItemEntity>, IEntityValidation
{
    public Guid BasketId { get; private set; }

    public Guid ProductId { get; set; }

    public int Quantity { get; set; }

    #region Related Data

    public BasketEntity Basket { get; private set; }

    public ProductEntity Product { get; private set; }

    #endregion Related Data

    #region Methods

    public void Update(BasketItemEntity entity)
    {
        ProductId = entity.ProductId;
        Quantity = entity.Quantity;
    }

    public void Validate()
    {
        ValidateProductId();
        ValidateQuantity();
    }

    private void ValidateProductId()
    {
        if (ProductId == Guid.Empty)
            throw new PropertyWasEmptyException(nameof(ProductId));
    }

    private void ValidateQuantity()
    {
        if (Quantity < 0)
            throw new PropertyWasNegativeException(nameof(Quantity));
    }

    #endregion Methods
}