using Microsoft.EntityFrameworkCore;
using Product.Core.Interfaces.Repositories;
using Product.Domain.Entities;
using Shared.Infrastructure.Bases;

namespace Product.Infrastructure.Repositories;

internal class ProductRepository(ProductContext context) : BaseRepository<ProductContext, ProductEntity>(context), IProductRepository
{
    public async Task<ProductEntity> UpdateAsync(Guid id, ProductEntity entity, CancellationToken cancellationToken)
    {
        var entityToUpdate = await _context.Set<ProductEntity>()
            .Include(x => x.ProductPhotos)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

        if (entityToUpdate == null)
            return null;

        entityToUpdate.Update(entity);
        entityToUpdate.Validate();

        await _context.SaveChangesAsync(cancellationToken);

        return entityToUpdate;
    }
}