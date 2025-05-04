using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Interfaces;
using Shop.Domain.Entities;

namespace Shop.Infrastructure.Repositories;

public interface IProductBaseRepository : IBaseRepository<ProductBaseEntity>, IUpdateRepository<ProductBaseEntity>
{
    Task<Guid?> GetIdByExternalIdAsync(Guid externalId, CancellationToken cancellationToken);
}

public class ProductBaseRepository(ShopContext context) : BaseRepository<ShopContext, ProductBaseEntity>(context), IProductBaseRepository
{
    public Task<Guid?> GetIdByExternalIdAsync(Guid externalId, CancellationToken cancellationToken)
        => _context.Set<ProductBaseEntity>().Where(x => x.ExternalId == externalId).Select(x => (Guid?)x.Id).FirstOrDefaultAsync(cancellationToken);

    public async Task<ProductBaseEntity> UpdateAsync(Guid id, ProductBaseEntity entity, CancellationToken cancellationToken)
    {
        var entityToUpdate = await _context.Set<ProductBaseEntity>()
            .Include(x => x.ProductParameters)
                .ThenInclude(x => x.Translations)
        .Include(x => x.Products)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (entity == null)
            return null;

        entityToUpdate.Update(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entityToUpdate;
    }
}