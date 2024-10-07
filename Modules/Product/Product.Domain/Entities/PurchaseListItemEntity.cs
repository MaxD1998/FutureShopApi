using Shared.Domain.Bases;

namespace Product.Domain.Entities;

public class PurchaseListItemEntity : BaseEntity
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
        PurchaseListId = entity.PurchaseListId;
    }
}