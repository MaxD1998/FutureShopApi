using Shared.Core.Interfaces;
using Shop.Domain.Entities.Products;

namespace Shop.Core.Interfaces.Repositories;

public interface IProductReviewRepository : IBaseRepository<ProductReviewEntity>, IUpdateRepository<ProductReviewEntity>
{
    Task<bool> AnyByUserIdAndProductIdAsync(Guid userId, Guid productId, CancellationToken cancellationToken);
}