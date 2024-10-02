using Shared.Domain.Bases;

namespace Product.Domain.Entities;

public class UserEntity : BaseEntity
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    #region Related Data

    public ICollection<PurchaseListEntity> PurchaseLists { get; set; }

    #endregion Related Data
}