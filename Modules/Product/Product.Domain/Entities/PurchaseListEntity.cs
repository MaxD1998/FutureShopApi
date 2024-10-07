using Shared.Domain.Bases;

namespace Product.Domain.Entities;

public class PurchaseListEntity : BaseEntity
{
    public bool IsFavourite { get; set; }

    public string Name { get; set; }

    public Guid? UserId { get; set; }

    #region Related Data

    public ICollection<PurchaseListItemEntity> PurchaseListItems { get; set; } = [];

    #endregion Related Data

    public void Update(PurchaseListEntity entity)
    {
        IsFavourite = entity.IsFavourite;
        Name = entity.Name;
        UserId = entity.UserId;
    }

    private void UpdatePurchaseListItems(ICollection<PurchaseListItemEntity> purchaseListItems)
    {
        var ids = purchaseListItems.Where(x => x.Id != Guid.Empty).Select(x => x.Id).ToList();
        PurchaseListItems = PurchaseListItems.Where(x => ids.Contains(x.Id)).ToList();

        foreach (var entity in PurchaseListItems)
        {
            var purchaseListItem = purchaseListItems.First(x => x.Id == entity.Id);
            entity.Update(purchaseListItem);
        }

        foreach (var purchaseListItem in purchaseListItems.Where(x => x.Id == Guid.Empty))
        {
        }
    }
}