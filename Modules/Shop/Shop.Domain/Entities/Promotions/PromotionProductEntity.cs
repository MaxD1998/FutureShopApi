using Shared.Domain.Bases;
using Shared.Domain.Exceptions;
using Shared.Domain.Interfaces;
using Shop.Domain.Entities.Products;

namespace Shop.Domain.Entities.Promotions;

public class PromotionProductEntity : BaseEntity, IUpdate<PromotionProductEntity>, IEntityValidation
{
    public Guid ProductId { get; set; }

    public Guid PromotionId { get; private set; }

    #region Related Data

    public ProductEntity Product { get; private set; }

    public PromotionEntity Promotion { get; private set; }

    #endregion Related Data

    #region Methods

    public void Update(PromotionProductEntity entity)
    {
    }

    public void Validate()
    {
        ValidateProductId();
    }

    private void ValidateProductId()
    {
        if (ProductId == Guid.Empty)
            throw new PropertyWasEmptyException(nameof(ProductId));
    }

    #endregion Methods
}