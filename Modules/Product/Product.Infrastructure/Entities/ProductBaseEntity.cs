using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Interfaces;

namespace Product.Infrastructure.Entities;

public class ProductBaseEntity : BaseEntity, IUpdate<ProductBaseEntity>
{
    public Guid CategoryId { get; set; }

    public string Name { get; set; }

    #region Related Data

    public CategoryEntity Category { get; set; }

    public ICollection<ProductEntity> Products { get; set; } = [];

    #endregion Related Data

    public void Update(ProductBaseEntity entity)
    {
        CategoryId = entity.CategoryId;
        Name = entity.Name;
    }
}