using Shared.Domain.Bases;
using Shared.Domain.Extensions;
using Shared.Domain.Interfaces;

namespace Shop.Domain.Entities;

public class ProductBaseEntity : BaseEntity, IUpdate<ProductBaseEntity>
{
    public Guid CategoryId { get; set; }

    #region Related Data

    public CategoryEntity Category { get; set; }

    public ICollection<ProductParameterEntity> ProductParameters { get; set; } = [];

    public ICollection<ProductEntity> Products { get; set; } = [];

    #endregion Related Data

    public void Update(ProductBaseEntity entity)
    {
        CategoryId = entity.CategoryId;
        ProductParameters.UpdateEntities(entity.ProductParameters);
    }
}