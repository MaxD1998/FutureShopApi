using Shared.Infrastructure.Interfaces;
using Shop.Infrastructure.Entities.Products;

namespace Shop.Infrastructure.Models.Products;

public class GetProductListByCategoryIdParams
{
    public Guid CategoryId { get; set; }

    public IFilter<ProductEntity> Filter { get; set; }

    public string Lang { get; set; }

    public Guid? UserId { get; set; }
}