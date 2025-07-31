using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Interfaces;
using Shop.Domain.Aggregates.Baskets;
using System.Linq.Expressions;

namespace Shop.Infrastructure.Repositories;

public interface IBasketRepository : IBaseRepository<BasketAggregate>, IUpdateRepository<BasketAggregate>
{
    Task<TResult> GetByUserIdAsync<TResult>(Guid userId, Expression<Func<BasketAggregate, TResult>> map, CancellationToken cancellationToken);
}

public class BasketRepository(ShopContext context) : BaseRepository<ShopContext, BasketAggregate>(context), IBasketRepository
{
    public async Task<TResult> GetByUserIdAsync<TResult>(Guid userId, Expression<Func<BasketAggregate, TResult>> map, CancellationToken cancellationToken)
        => await _context.Set<BasketAggregate>().AsNoTracking().Where(x => x.UserId == userId).Select(map).FirstOrDefaultAsync(cancellationToken);

    public async Task<BasketAggregate> UpdateAsync(Guid id, BasketAggregate entity, CancellationToken cancellationToken)
    {
        var entityToUpdate = await _context.Set<BasketAggregate>()
            .Include(x => x.BasketItems)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (entityToUpdate == null)
            return null;

        entityToUpdate.Update(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entityToUpdate;
    }
}