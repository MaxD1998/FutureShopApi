using Microsoft.EntityFrameworkCore;
using Product.Infrastructure.Entities;
using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Interfaces;

namespace Product.Infrastructure.Repositories;

public interface IProductBaseRepository : IBaseRepository<ProductBaseEntity>, IUpdateRepository<ProductBaseEntity>
{
}

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

        await _context.SaveChangesAsync(cancellationToken);

        return entityToUpdate;
    }
}