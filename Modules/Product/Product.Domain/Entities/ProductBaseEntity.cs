using Shared.Domain.Bases;

namespace Product.Domain.Entities;

public class ProductBaseEntity : BaseEntity
{
    public Guid CategoryId { get; set; }

    public string Description { get; set; }

    public string Name { get; set; }

    #region Related Data

    public CategoryEntity Category { get; set; }

    public ICollection<ProductParameterEntity> ProductParameters { get; set; } = [];

    public ICollection<ProductEntity> Products { get; set; } = [];

    #endregion Related Data
}