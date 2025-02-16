using Shop.Domain.Entities;
using System.Linq.Expressions;

namespace Shop.Core.Dtos.ProductParameter;

public class ProductParameterFlatDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public Guid? ProductParameterValueId { get; set; }

    public string Value { get; set; }

    public static Expression<Func<ProductParameterEntity, ProductParameterFlatDto>> Map(Guid productId) => entity => new ProductParameterFlatDto
    {
        Id = entity.Id,
        Name = entity.Name,
        ProductParameterValueId = entity.ProductParameterValues.Where(x => x.ProductId == productId).Select(x => x.Id).FirstOrDefault(),
        Value = entity.ProductParameterValues.Where(x => x.ProductId == productId).Select(x => x.Value).FirstOrDefault(),
    };
}