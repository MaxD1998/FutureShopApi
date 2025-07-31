using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Interfaces;
using Shop.Domain.Aggregates.ProductBases;

namespace Shop.Infrastructure.Repositories;

public interface IProductBaseRepository : IBaseRepository<ProductBaseAggregate>, IUpdateRepository<ProductBaseAggregate>
{
    Task CreateOrUpdateAsync(ProductBaseAggregate entity, CancellationToken cancellationToken);

    Task DeleteByExternalIdAsync(Guid externalId, CancellationToken cancellationToken);

    Task<Guid?> GetIdByExternalIdAsync(Guid externalId, CancellationToken cancellationToken);
}

public class ProductBaseRepository(ShopContext context) : BaseRepository<ShopContext, ProductBaseAggregate>(context), IProductBaseRepository
{
    public async Task CreateOrUpdateAsync(ProductBaseAggregate eventEntity, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<ProductBaseAggregate>()
            .FirstOrDefaultAsync(x => x.ExternalId == eventEntity.ExternalId, cancellationToken);

        if (entity is null)
            await _context.Set<ProductBaseAggregate>().AddAsync(eventEntity, cancellationToken);
        else
            entity.UpdateEvent(eventEntity);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public Task DeleteByExternalIdAsync(Guid externalId, CancellationToken cancellationToken)
        => _context.Set<ProductBaseAggregate>().Where(x => x.ExternalId == externalId).ExecuteDeleteAsync(cancellationToken);

    public Task<Guid?> GetIdByExternalIdAsync(Guid externalId, CancellationToken cancellationToken)
        => _context.Set<ProductBaseAggregate>().Where(x => x.ExternalId == externalId).Select(x => (Guid?)x.Id).FirstOrDefaultAsync(cancellationToken);

    public async Task<ProductBaseAggregate> UpdateAsync(Guid id, ProductBaseAggregate entity, CancellationToken cancellationToken)
    {
        var entityToUpdate = await _context.Set<ProductBaseAggregate>()
            .Include(x => x.ProductParameters)
                .ThenInclude(x => x.Translations)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (entity == null)
            return null;

        entityToUpdate.Update(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entityToUpdate;
    }
}