using Shared.Infrastructure.Interfaces;
using Shop.Domain.Entities;

namespace Shop.Infrastructure.Models.Product;

public class GetProductListByCategoryIdParams
{
    public Guid CategoryId { get; set; }

    public IFilter<ProductEntity> Filter { get; set; }

    public string Lang { get; set; }

    public Guid? UserId { get; set; }
}