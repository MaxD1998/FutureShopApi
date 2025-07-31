using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Interfaces;
using Shop.Domain.Aggregates.Categories;

namespace Shop.Infrastructure.Repositories;

public interface ICategoryRepository : IBaseRepository<CategoryAggregate>, IUpdateRepository<CategoryAggregate>
{
    Task CreateOrUpdateForEventAsync(CategoryAggregate eventEntity, CancellationToken cancellationToken);

    Task DeleteByExternalIdAsync(Guid externalId, CancellationToken cancellationToken);

    Task<Guid?> GetIdByExternalIdAsync(Guid externalId, CancellationToken cancellationToken);

    Task<List<CategoryAggregate>> GetListByExternalIdsAsync(List<Guid> ids, CancellationToken cancellationToken);
}

public class CategoryRepository(ShopContext context) : BaseRepository<ShopContext, CategoryAggregate>(context), ICategoryRepository
{
    public async Task CreateOrUpdateForEventAsync(CategoryAggregate eventEntity, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<CategoryAggregate>()
              .Include(x => x.SubCategories)
              .FirstOrDefaultAsync(x => x.ExternalId == eventEntity.ExternalId, cancellationToken);

        if (entity is null)
            await _context.Set<CategoryAggregate>().AddAsync(eventEntity, cancellationToken);
        else
            entity.UpdateEvent(eventEntity);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public Task DeleteByExternalIdAsync(Guid externalId, CancellationToken cancellationToken)
        => _context.Set<CategoryAggregate>().Where(x => x.ExternalId == externalId).ExecuteDeleteAsync(cancellationToken);

    public Task<Guid?> GetIdByExternalIdAsync(Guid externalId, CancellationToken cancellationToken)
        => _context.Set<CategoryAggregate>().Where(x => x.ExternalId == externalId).Select(x => (Guid?)x.Id).FirstOrDefaultAsync(cancellationToken);

    public Task<List<CategoryAggregate>> GetListByExternalIdsAsync(List<Guid> ids, CancellationToken cancellationToken)
        => _context.Set<CategoryAggregate>().Where(x => ids.Contains(x.ExternalId)).ToListAsync(cancellationToken);

    public async Task<CategoryAggregate> UpdateAsync(Guid id, CategoryAggregate entity, CancellationToken cancellationToken)
    {
        var entityToUpdate = await _context.Set<CategoryAggregate>()
            .Include(x => x.Translations)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (entityToUpdate == null)
            return null;

        entityToUpdate.Update(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entityToUpdate;
    }
}