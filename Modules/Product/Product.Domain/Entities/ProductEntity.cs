using Shared.Domain.Bases;

namespace Product.Domain.Entities;

public class ProductEntity : BaseEntity
{
    public string Name { get; set; }

    public Guid ProductBaseId { get; set; }

    #region Related Data

    public ProductBaseEntity ProductBase { get; set; }

    public ICollection<ProductParameterValueEntity> ProductParameterValues { get; set; }

    #endregion Related Data
}