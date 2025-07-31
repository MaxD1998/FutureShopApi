using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Interfaces;
using Shop.Domain.Aggregates.PurchaseLists;
using System.Linq.Expressions;

namespace Shop.Infrastructure.Repositories;

public interface IPurchaseListRepository : IBaseRepository<PurchaseListAggregate>, IUpdateRepository<PurchaseListAggregate>
{
    Task<List<TResult>> GetByUserIdAsync<TResult>(Guid userId, Expression<Func<PurchaseListAggregate, TResult>> map, CancellationToken cancellationToken);
}

public class PurchaseListRepository(ShopContext context) : BaseRepository<ShopContext, PurchaseListAggregate>(context), IPurchaseListRepository
{
    public override async Task<PurchaseListAggregate> CreateAsync(PurchaseListAggregate entity, CancellationToken cancellationToken)
    {
        if (entity.UserId.HasValue && entity.IsFavourite)
        {
            var hasFavourite = await _context.Set<PurchaseListAggregate>().AnyAsync(x => x.UserId == entity.UserId && x.IsFavourite, cancellationToken);

            if (hasFavourite)
                return null;
        }

        await _context.Set<PurchaseListAggregate>().AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public Task<List<TResult>> GetByUserIdAsync<TResult>(Guid userId, Expression<Func<PurchaseListAggregate, TResult>> map, CancellationToken cancellationToken)
        => _context.Set<PurchaseListAggregate>()
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.IsFavourite)
                .ThenBy(x => x.Name)
            .Select(map)
            .ToListAsync(cancellationToken);

    public async Task<PurchaseListAggregate> UpdateAsync(Guid id, PurchaseListAggregate entity, CancellationToken cancellationToken)
    {
        if (entity.UserId.HasValue && entity.IsFavourite)
        {
            var hasFavourite = await _context.Set<PurchaseListAggregate>().AnyAsync(x => x.Id != id && x.UserId == entity.UserId && x.IsFavourite, cancellationToken);

            if (hasFavourite)
                return null;
        }

        var entityToUpdate = await _context.Set<PurchaseListAggregate>()
            .Include(x => x.PurchaseListItems)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (entity == null)
            return null;

        entityToUpdate.Update(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entityToUpdate;
    }
}