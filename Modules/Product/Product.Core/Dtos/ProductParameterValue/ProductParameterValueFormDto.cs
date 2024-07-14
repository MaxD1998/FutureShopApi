using Product.Domain.Entities;

namespace Product.Core.Dtos.ProductParameterValue;

public class ProductParameterValueFormDto
{
    public ProductParameterValueFormDto()
    {
    }

    public ProductParameterValueFormDto(ProductParameterValueEntity entity)
    {
        ProductParameterId = entity.ProductParameterId;
        Value = entity.Value;
    }

    public Guid ProductParameterId { get; set; }

    public string Value { get; set; }

    public ProductParameterValueEntity ToEntity() => new()
    {
        ProductParameterId = ProductParameterId,
        Value = Value,
    };
}