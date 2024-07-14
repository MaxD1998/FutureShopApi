using Product.Domain.Entities;

namespace Product.Core.Dtos.Product;

public class ProductListDto
{
    public ProductListDto(ProductEntity entity)
    {
        FilledPropertyCount = $"{entity.ProductParameterValues?.Count ?? 0}/{entity.ProductBase?.ProductParameters?.Count ?? 0}";
        Id = entity.Id;
        Name = entity.Name;
        Price = entity.Price;
    }

    public string FilledPropertyCount { get; set; }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }
}