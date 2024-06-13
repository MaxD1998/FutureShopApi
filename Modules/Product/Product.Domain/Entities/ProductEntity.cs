using Shared.Domain.Bases;

namespace Product.Domain.Entities;

public class ProductEntity : BaseEntity
{
    public string Description { get; set; }

    public string Name { get; set; }

    public double Price { get; set; }

    public Guid ProductBaseId { get; set; }

    #region Related Data

    public ProductBaseEntity ProductBase { get; set; }

    public ICollection<ProductParameterValueEntity> ProductParameterValues { get; set; } = [];

    #endregion Related Data
}