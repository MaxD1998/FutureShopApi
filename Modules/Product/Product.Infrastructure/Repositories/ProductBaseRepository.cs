using Microsoft.EntityFrameworkCore;
using Product.Core.Interfaces.Repositories;
using Product.Domain.Entities;
using Shared.Infrastructure.Bases;

namespace Product.Infrastructure.Repositories;

internal class ProductBaseRepository(ProductContext context) : BaseRepository<ProductContext, ProductBaseEntity>(context), IProductBaseRepository
{
    public async Task<ProductBaseEntity> UpdateAsync(Guid id, ProductBaseEntity entity, CancellationToken cancellationToken)
    {
        var entityToUpdate = await _context.Set<ProductBaseEntity>()
            .Include(x => x.Products)
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