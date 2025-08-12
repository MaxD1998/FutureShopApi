using Shared.Domain.Bases;
using Shared.Domain.Extensions;
using Shared.Domain.Interfaces;
using Shop.Domain.Aggregates.Categories;
using Shop.Domain.Aggregates.ProductBases.Entities;
using Shop.Domain.Aggregates.Products;

namespace Shop.Domain.Aggregates.ProductBases;

public class ProductBaseAggregate : BaseExternalEntity, IUpdate<ProductBaseAggregate>, IUpdateEvent<ProductBaseAggregate>
{
    public Guid CategoryId { get; set; }

    public string Name { get; set; }

    #region Related Data

    public CategoryAggregate Category { get; set; }

    public ICollection<ProductParameterEntity> ProductParameters { get; set; } = [];

    public ICollection<ProductAggregate> Products { get; set; } = [];

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

    public void Update(ProductBaseAggregate entity)
    {
        ProductParameters.UpdateEntities(entity.ProductParameters.ToList());
    }

    public void UpdateEvent(ProductBaseAggregate entity)
    {
        CategoryId = entity.CategoryId;
    }
}