using Shared.Domain.Bases;
using Shared.Domain.Interfaces;
using Shop.Domain.Aggregates.ProductBases.Entities;
using Shop.Domain.Aggregates.Products;

namespace Shop.Domain.Aggregates.Products.Entities;

public class ProductParameterValueEntity : BaseEntity, IUpdate<ProductParameterValueEntity>
{
    public Guid ProductId { get; set; }

    public Guid ProductParameterId { get; set; }

    public string Value { get; set; }

    #region Related Data

    public ProductAggregate Product { get; set; }

    public ProductParameterEntity ProductParameter { get; set; }

    #endregion Related Data

    public void Update(ProductParameterValueEntity entity)
    {
        Value = entity.Value;
    }
}