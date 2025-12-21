using Shared.Shared.Interfaces;
using Shop.Domain.Entities.Products;

namespace Shop.Core.Models.Products;

public class GetProductListByCategoryIdParams
{
    public Guid CategoryId { get; set; }

    public IFilter<ProductEntity> Filter { get; set; }

    public string Lang { get; set; }

    public Guid? UserId { get; set; }
}