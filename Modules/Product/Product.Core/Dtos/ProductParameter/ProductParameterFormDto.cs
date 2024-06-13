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
        ProductBaseId = entity.ProductBaseId;
    }

    public string Name { get; set; }

    public Guid ProductBaseId { get; set; }
}