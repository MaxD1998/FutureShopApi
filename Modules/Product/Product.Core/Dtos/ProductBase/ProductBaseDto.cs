using Product.Domain.Entities;

namespace Product.Core.Dtos.ProductBase;

public class ProductBaseDto
{
    public ProductBaseDto(ProductBaseEntity entity)
    {
        CategoryName = entity.Category.Name;
        Id = entity.Id;
        Name = entity.Name;
        ProductCount = entity.Products.Count();
        ProductParameterCount = entity.ProductParameters.Count();
    }

    public string CategoryName { get; }

    public Guid Id { get; set; }

    public string Name { get; }

    public int ProductCount { get; }

    public int ProductParameterCount { get; }
}