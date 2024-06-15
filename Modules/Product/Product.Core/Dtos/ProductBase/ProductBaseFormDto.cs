using Product.Core.Dtos.Product;
using Product.Core.Dtos.ProductParameter;
using Product.Domain.Entities;

namespace Product.Core.Dtos.ProductBase;

public class ProductBaseFormDto
{
    public ProductBaseFormDto()
    {
    }

    public ProductBaseFormDto(ProductBaseEntity entity)
    {
        CategoryId = entity.CategoryId;
        Name = entity.Name;
        ProductParameters = entity.ProductParameters.Select(x => new ProductParameterFormDto(x)).ToList();
    }

    public Guid CategoryId { get; set; }

    public string Name { get; set; }

    public List<ProductParameterFormDto> ProductParameters { get; set; }

    public List<ProductFormDto> Products { get; set; }

    public ProductBaseEntity ToEntity() => new()
    {
        CategoryId = CategoryId,
        Name = Name
    };
}