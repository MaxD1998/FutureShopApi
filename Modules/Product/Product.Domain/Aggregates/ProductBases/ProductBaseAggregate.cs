using Product.Domain.Aggregates.Categories;
using Product.Domain.Aggregates.Products;
using Shared.Domain.Bases;
using Shared.Domain.Interfaces;

namespace Product.Domain.Aggregates.ProductBases;

public class ProductBaseAggregate : BaseEntity, IUpdate<ProductBaseAggregate>
{
    public ProductBaseAggregate(Guid categoryId, string name)
    {
        SetCategoryId(categoryId);
        SetName(name);
    }

    private ProductBaseAggregate()
    {
    }

    public Guid CategoryId { get; private set; }

    public string Name { get; private set; }

    #region Related Data

    public CategoryAggregate Category { get; private set; }

    public IReadOnlyCollection<ProductAggregate> Products { get; private set; } = [];

    #endregion Related Data

    #region Setters

    private void SetCategoryId(Guid categoryId)
    {
        ValidateRequiredProperty(nameof(CategoryId), categoryId);

        CategoryId = categoryId;
    }

    private void SetName(string name)
    {
        ValidateRequiredLongStringProperty(nameof(Name), name);

        Name = name;
    }

    #endregion Setters

    #region Methods

    public void Update(ProductBaseAggregate entity)
    {
        CategoryId = entity.CategoryId;
        Name = entity.Name;
    }

    #endregion Methods
}