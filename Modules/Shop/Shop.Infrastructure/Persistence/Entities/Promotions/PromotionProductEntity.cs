using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Exceptions;
using Shared.Infrastructure.Interfaces;
using Shop.Infrastructure.Persistence.Entities.Products;

namespace Shop.Infrastructure.Persistence.Entities.Promotions;

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