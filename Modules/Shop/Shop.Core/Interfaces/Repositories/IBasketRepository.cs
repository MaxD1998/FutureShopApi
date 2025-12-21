using Shared.Core.Interfaces;
using Shop.Domain.Entities.Baskets;
using System.Linq.Expressions;

namespace Shop.Core.Interfaces.Repositories;

public interface IBasketRepository : IBaseRepository<BasketEntity>, IUpdateRepository<BasketEntity>
{
    Task<TResult> GetByUserIdAsync<TResult>(Guid userId, Expression<Func<BasketEntity, TResult>> map, CancellationToken cancellationToken);
}