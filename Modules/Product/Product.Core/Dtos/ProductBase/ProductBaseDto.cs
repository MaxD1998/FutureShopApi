using Product.Domain.Entities;

namespace Product.Core.Dtos.ProductBase;

public class ProductBaseDto
{
    public ProductBaseDto(ProductBaseEntity entity)
    {
        CategoryName = entity.Category.Name;
        Name = entity.Name;
        ProductCount = entity.Products.Count();
        ProductParameterCount = entity.ProductParameters.Count();
    }

    public string CategoryName { get; set; }

    public string Name { get; set; }

    public int ProductCount { get; set; }

    public int ProductParameterCount { get; set; }
}