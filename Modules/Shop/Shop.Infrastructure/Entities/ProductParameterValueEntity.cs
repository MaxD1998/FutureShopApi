using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Interfaces;

namespace Shop.Infrastructure.Entities;

public class ProductParameterValueEntity : BaseEntity, IUpdate<ProductParameterValueEntity>
{
    public Guid ProductId { get; set; }

    public Guid ProductParameterId { get; set; }

    public string Value { get; set; }

    #region Related Data

    public ProductEntity Product { get; set; }

    public ProductParameterEntity ProductParameter { get; set; }

    #endregion Related Data

    public void Update(ProductParameterValueEntity entity)
    {
        Value = entity.Value;
    }
}