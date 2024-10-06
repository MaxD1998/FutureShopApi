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
}