using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Interfaces;
using Shop.Infrastructure.Persistence;
using Shop.Domain.Entities.Baskets;
using System.Linq.Expressions;

namespace Shop.Infrastructure.Persistence.Repositories;

public interface IBasketRepository : IBaseRepository<BasketEntity>, IUpdateRepository<BasketEntity>
{
    Task<TResult> GetByUserIdAsync<TResult>(Guid userId, Expression<Func<BasketEntity, TResult>> map, CancellationToken cancellationToken);
}

internal class BasketRepository(ShopContext context) : BaseRepository<ShopContext, BasketEntity>(context), IBasketRepository
{
    public async Task<TResult> GetByUserIdAsync<TResult>(Guid userId, Expression<Func<BasketEntity, TResult>> map, CancellationToken cancellationToken)
        => await _context.Set<BasketEntity>().AsNoTracking().Where(x => x.UserId == userId).Select(map).FirstOrDefaultAsync(cancellationToken);

    public async Task<BasketEntity> UpdateAsync(Guid id, BasketEntity entity, CancellationToken cancellationToken)
    {
        var entityToUpdate = await _context.Set<BasketEntity>()
            .Include(x => x.BasketItems)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (entityToUpdate == null)
            return null;

        entityToUpdate.Update(entity);
        entityToUpdate.Validate();

        await _context.SaveChangesAsync(cancellationToken);

        return entityToUpdate;
    }
}