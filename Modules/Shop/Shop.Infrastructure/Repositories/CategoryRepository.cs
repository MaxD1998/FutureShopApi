using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Interfaces;
using Shop.Infrastructure.Entities.Categories;

namespace Shop.Infrastructure.Repositories;

public interface ICategoryRepository : IBaseRepository<CategoryEntity>, IUpdateRepository<CategoryEntity>
{
    Task CreateOrUpdateForEventAsync(CategoryEntity eventEntity, CancellationToken cancellationToken);

    Task DeleteByExternalIdAsync(Guid externalId, CancellationToken cancellationToken);

    Task<Guid?> GetIdByExternalIdAsync(Guid externalId, CancellationToken cancellationToken);

    Task<List<CategoryEntity>> GetListByExternalIdsAsync(List<Guid> ids, CancellationToken cancellationToken);
}

internal class CategoryRepository(ShopContext context) : BaseRepository<ShopContext, CategoryEntity>(context), ICategoryRepository
{
    public async Task CreateOrUpdateForEventAsync(CategoryEntity eventEntity, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<CategoryEntity>()
              .Include(x => x.SubCategories)
              .FirstOrDefaultAsync(x => x.ExternalId == eventEntity.ExternalId, cancellationToken);

        if (entity is null)
            await _context.Set<CategoryEntity>().AddAsync(eventEntity, cancellationToken);
        else
            entity.UpdateEvent(eventEntity);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public Task DeleteByExternalIdAsync(Guid externalId, CancellationToken cancellationToken)
        => _context.Set<CategoryEntity>().Where(x => x.ExternalId == externalId).ExecuteDeleteAsync(cancellationToken);

    public Task<Guid?> GetIdByExternalIdAsync(Guid externalId, CancellationToken cancellationToken)
        => _context.Set<CategoryEntity>().Where(x => x.ExternalId == externalId).Select(x => (Guid?)x.Id).FirstOrDefaultAsync(cancellationToken);

    public Task<List<CategoryEntity>> GetListByExternalIdsAsync(List<Guid> ids, CancellationToken cancellationToken)
        => _context.Set<CategoryEntity>().Where(x => ids.Contains(x.ExternalId)).ToListAsync(cancellationToken);

    public async Task<CategoryEntity> UpdateAsync(Guid id, CategoryEntity entity, CancellationToken cancellationToken)
    {
        var entityToUpdate = await _context.Set<CategoryEntity>()
            .Include(x => x.Translations)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (entityToUpdate == null)
            return null;

        entityToUpdate.Update(entity);
        entityToUpdate.Validate();

        await _context.SaveChangesAsync(cancellationToken);

        return entityToUpdate;
    }
}