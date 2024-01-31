using Shared.Domain.Bases;

namespace Product.Domain.Entities;

public class ProductParameterValueEntity : BaseEntity
{
    public Guid ProductId { get; set; }

    public Guid ProductParameterId { get; set; }

    public string Value { get; set; }

    #region Related Data

    public ProductEntity Product { get; set; }

    public ProductParameterEntity ProductParameter { get; set; }

    #endregion Related Data
}