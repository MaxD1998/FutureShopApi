using Product.Domain.Entities;

namespace Product.Core.Dtos.ProductParameter;

public class ProductParameterFormDto
{
    public ProductParameterFormDto()
    {
    }

    public ProductParameterFormDto(ProductParameterEntity entity)
    {
        Name = entity.Name;
    }

    public string Name { get; set; }
}