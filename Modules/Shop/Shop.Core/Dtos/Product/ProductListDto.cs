using Shop.Domain.Entities.Products;
using System.Linq.Expressions;

namespace Shop.Core.Dtos.Product;

public class ProductListDto
{
    public string FilledParameters { get; set; }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public static Expression<Func<ProductEntity, ProductListDto>> Map()
    {
        var utcNow = DateTime.UtcNow;

        return entity => new()
        {
            FilledParameters = $"{entity.ProductParameterValues.Count}/{entity.ProductBase.ProductParameters.Count}",
            Id = entity.Id,
            Name = entity.Name,
            Price = entity.Prices.AsQueryable().Where(x => (!x.Start.HasValue || x.Start <= utcNow) && (!x.End.HasValue || utcNow < x.End)).Select(x => x.Price).FirstOrDefault(),
        };
    }
}