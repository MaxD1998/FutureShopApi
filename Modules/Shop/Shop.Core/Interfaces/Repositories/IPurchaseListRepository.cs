using Shared.Core.Interfaces;
using Shop.Domain.Entities.PurchaseLists;
using System.Linq.Expressions;

namespace Shop.Core.Interfaces.Repositories;

public interface IPurchaseListRepository : IBaseRepository<PurchaseListEntity>, IUpdateRepository<PurchaseListEntity>
{
    Task<List<TResult>> GetByUserIdAsync<TResult>(Guid userId, Expression<Func<PurchaseListEntity, TResult>> map, CancellationToken cancellationToken);
}