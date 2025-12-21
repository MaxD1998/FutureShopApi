using Shared.Domain.Bases;
using Shared.Domain.Exceptions;
using Shared.Domain.Interfaces;
using Shop.Infrastructure.Persistence.Entities.Products;

namespace Shop.Infrastructure.Persistence.Entities.PurchaseLists;

public class PurchaseListItemEntity : BaseEntity, IUpdate<PurchaseListItemEntity>, IEntityValidation
{
    public Guid ProductId { get; set; }

    public Guid PurchaseListId { get; private set; }

    #region Related Data

    public ProductEntity Product { get; private set; }

    public PurchaseListEntity PurchaseList { get; private set; }

    #endregion Related Data

    #region Methods

    public void Update(PurchaseListItemEntity entity)
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