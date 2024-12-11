using FluentValidation;
using Product.Domain.Entities;
using System.Linq.Expressions;

namespace Product.Core.Dtos.ProductParameterValue;

public class ProductParameterValueFormDto
{
    public Guid? Id { get; set; }

    public Guid ProductParameterId { get; set; }

    public string Value { get; set; }

    public static Expression<Func<ProductParameterValueEntity, ProductParameterValueFormDto>> Map() => entity => new()
    {
        Id = entity.Id,
        ProductParameterId = entity.ProductParameterId,
        Value = entity.Value,
    };

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