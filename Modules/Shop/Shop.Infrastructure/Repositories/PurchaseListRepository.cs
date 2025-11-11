using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Interfaces;
using Shop.Infrastructure.Entities.PurchaseLists;
using System.Linq.Expressions;

namespace Shop.Infrastructure.Repositories;

public interface IPurchaseListRepository : IBaseRepository<PurchaseListEntity>, IUpdateRepository<PurchaseListEntity>
{
    Task<List<TResult>> GetByUserIdAsync<TResult>(Guid userId, Expression<Func<PurchaseListEntity, TResult>> map, CancellationToken cancellationToken);
}

internal class PurchaseListRepository(ShopContext context) : BaseRepository<ShopContext, PurchaseListEntity>(context), IPurchaseListRepository
{
    public override async Task<PurchaseListEntity> CreateAsync(PurchaseListEntity entity, CancellationToken cancellationToken)
    {
        if (entity.UserId.HasValue && entity.IsFavourite)
        {
            var hasFavourite = await _context.Set<PurchaseListEntity>().AnyAsync(x => x.UserId == entity.UserId && x.IsFavourite, cancellationToken);

            if (hasFavourite)
                return null;
        }

        entity.Validate();

        await _context.Set<PurchaseListEntity>().AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public Task<List<TResult>> GetByUserIdAsync<TResult>(Guid userId, Expression<Func<PurchaseListEntity, TResult>> map, CancellationToken cancellationToken)
        => _context.Set<PurchaseListEntity>()
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.IsFavourite)
                .ThenBy(x => x.Name)
            .Select(map)
            .ToListAsync(cancellationToken);

    public async Task<PurchaseListEntity> UpdateAsync(Guid id, PurchaseListEntity entity, CancellationToken cancellationToken)
    {
        if (entity.UserId.HasValue && entity.IsFavourite)
        {
            var hasFavourite = await _context.Set<PurchaseListEntity>().AnyAsync(x => x.Id != id && x.UserId == entity.UserId && x.IsFavourite, cancellationToken);

            if (hasFavourite)
                return null;
        }

        var entityToUpdate = await _context.Set<PurchaseListEntity>()
            .Include(x => x.PurchaseListItems)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (entityToUpdate == null)
            return null;

        entityToUpdate.Update(entity);
        entityToUpdate.Validate();

        await _context.SaveChangesAsync(cancellationToken);

        return entityToUpdate;
    }
}