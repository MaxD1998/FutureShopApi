using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Extensions;
using Shared.Infrastructure.Interfaces;

namespace Shop.Infrastructure.Entities;

public class ProductBaseEntity : BaseExternalEntity, IUpdate<ProductBaseEntity>, IUpdateEvent<ProductBaseEntity>
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
        ProductParameters.UpdateEntities(entity.ProductParameters);
    }

    public void UpdateEvent(ProductBaseEntity entity)
    {
        CategoryId = entity.CategoryId;
    }
}