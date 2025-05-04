using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Bases;
using Shop.Domain.Entities;
using System.Linq.Expressions;

namespace Shop.Infrastructure.Repositories;

public interface IBasketRepository : IBaseRepository<BasketEntity>
{
    Task<TResult> GetByUserIdAsync<TResult>(Guid userId, Expression<Func<BasketEntity, TResult>> map, CancellationToken cancellationToken)
}

public class BasketRepository(ShopContext context) : BaseRepository<ShopContext, BasketEntity>(context), IBasketRepository
{
    public async Task<TResult> GetByUserIdAsync<TResult>(Guid userId, Expression<Func<BasketEntity, TResult>> map, CancellationToken cancellationToken)
        => await _context.Set<BasketEntity>().AsNoTracking().Where(x => x.UserId == userId).Select(map).FirstOrDefaultAsync(cancellationToken);
}