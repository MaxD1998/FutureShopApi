using Shared.Domain.Bases;
using Shared.Domain.Extensions;
using Shared.Domain.Interfaces;

namespace Product.Domain.Entities;

public class ProductBaseEntity : BaseEntity, IUpdate<ProductBaseEntity>
{
    public Guid CategoryId { get; set; }

    public string Name { get; set; }

    #region Related Data

    public CategoryEntity Category { get; set; }

    public ICollection<ProductParameterEntity> ProductParameters { get; set; } = [];

    public ICollection<ProductEntity> Products { get; set; } = [];

    #endregion Related Data

    public void Update(ProductBaseEntity entity)
    {
        CategoryId = entity.CategoryId;
        Name = entity.Name;
        ProductParameters.UpdateEntities(entity.ProductParameters);
    }
}