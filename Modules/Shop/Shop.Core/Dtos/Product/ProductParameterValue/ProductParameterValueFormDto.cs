using Shop.Infrastructure.Persistence.Entities.Products;
using System.Linq.Expressions;

namespace Shop.Core.Dtos.Product.ProductParameterValue;

public class ProductParameterValueFormDto
{
    public Guid? Id { get; set; }

    public Guid ProductParameterId { get; set; }

    public string ProductParameterName { get; set; }

    public string Value { get; set; }

    public ProductParameterValueEntity ToEntity() => new()
    {
        Id = Id ?? Guid.Empty,
        ProductParameterId = ProductParameterId,
        Value = Value,
    };
}