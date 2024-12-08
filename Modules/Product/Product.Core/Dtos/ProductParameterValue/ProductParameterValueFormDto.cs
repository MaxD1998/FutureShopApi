using FluentValidation;
using Product.Domain.Entities;

namespace Product.Core.Dtos.ProductParameterValue;

public class ProductParameterValueFormDto
{
    public ProductParameterValueFormDto()
    {
    }

    public ProductParameterValueFormDto(ProductParameterValueEntity entity)
    {
        Id = entity.Id;
        ProductParameterId = entity.ProductParameterId;
        Value = entity.Value;
    }

    public Guid? Id { get; set; }

    public Guid ProductParameterId { get; set; }

    public string Value { get; set; }

    public ProductParameterValueEntity ToEntity() => new()
    {
        Id = Id ?? Guid.Empty,
        ProductParameterId = ProductParameterId,
        Value = Value,
    };
}

public class ProductParameterValueFormValidator : AbstractValidator<ProductParameterValueFormDto>
{
}