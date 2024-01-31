using Shared.Bases;

namespace Domain.Entities;

public class CategoryEntity : BaseEntity
{
    public string Name { get; set; }

    public Guid? ParentCategoryId { get; set; }

    #region Related Data

    public ICollection<ProductBaseEntity> ProductBases { get; set; }

    #endregion Related Data
}