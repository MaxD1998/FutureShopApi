using Shared.Infrastructure.Interfaces;
using Shop.Domain.Aggregates.Products;

namespace Shop.Infrastructure.Models.Product;

public class GetProductListByCategoryIdParams
{
    public Guid CategoryId { get; set; }

    public IFilter<ProductAggregate> Filter { get; set; }

    public string Lang { get; set; }

    public Guid? UserId { get; set; }
}