using Shared.Domain.Bases;
using Shared.Domain.Extensions;
using Shared.Domain.Interfaces;

namespace Shop.Domain.Entities;

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