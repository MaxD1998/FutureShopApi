using Shared.Domain.Bases;
using Shared.Domain.Interfaces;

namespace Warehouse.Domain.Entities;

public class ProductParameterEntity : BaseEntity, IUpdate<ProductParameterEntity>
{
    public string Name { get; set; }

    public Guid ProductBaseId { get; set; }

    #region Related Data

    public ProductBaseEntity ProductBase { get; set; }

    #endregion Related Data

    public void Update(ProductParameterEntity entity)
    {
        Name = entity.Name;
    }
}