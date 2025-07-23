using Product.Domain.Aggregates.Categories;
using Shared.Domain.Bases;
using Shared.Domain.Interfaces;

namespace Product.Domain.Aggregates.ProductBases;

public class ProductBaseAggregate : BaseEntity, IUpdate<ProductBaseAggregate>
{
    public Guid CategoryId { get; set; }

    public string Name { get; set; }

    #region Related Data

    public CategoryAggregate Category { get; set; }

    public ICollection<Products.ProductAggregate> Products { get; set; } = [];

    #endregion Related Data

    public void Update(ProductBaseAggregate entity)
    {
        CategoryId = entity.CategoryId;
        Name = entity.Name;
    }
}