using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Interfaces;

namespace Shop.Infrastructure.Entities;

public class PurchaseListItemEntity : BaseEntity, IUpdate<PurchaseListItemEntity>
{
    public Guid ProductId { get; set; }

    public Guid PurchaseListId { get; set; }

    #region Related Data

    public ProductEntity Product { get; set; }

    public PurchaseListEntity PurchaseList { get; set; }

    #endregion Related Data

    public void Update(PurchaseListItemEntity entity)
    {
        ProductId = entity.ProductId;
    }
}