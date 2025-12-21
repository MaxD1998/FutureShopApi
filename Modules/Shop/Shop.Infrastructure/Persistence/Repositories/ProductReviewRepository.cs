using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Bases;
using Shop.Core.Interfaces.Repositories;
using Shop.Domain.Entities.Products;

namespace Shop.Infrastructure.Persistence.Repositories;

internal class ProductReviewRepository(ShopContext context) : BaseRepository<ShopContext, ProductReviewEntity>(context), IProductReviewRepository
{
    public Task<bool> AnyByUserIdAndProductIdAsync(Guid userId, Guid productId, CancellationToken cancellationToken)
        => AnyAsync(x => x.UserId == userId && x.ProductId == productId, cancellationToken);

    public async Task<ProductReviewEntity> UpdateAsync(Guid id, ProductReviewEntity entity, CancellationToken cancellationToken)
    {
        var entityToUpdate = await _context.Set<ProductReviewEntity>()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (entityToUpdate == null)
            return null;

        entityToUpdate.Update(entity);
        entityToUpdate.Validate();

        await _context.SaveChangesAsync(cancellationToken);

        return entityToUpdate;
    }
}