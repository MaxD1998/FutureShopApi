using Shared.Core.Interfaces;
using Shop.Domain.Entities.Categories;
using System.Linq.Expressions;

namespace Shop.Core.Interfaces.Repositories;

public interface ICategoryRepository : IBaseRepository<CategoryEntity>, IUpdateRepository<CategoryEntity>
{
    Task CreateOrUpdateForEventAsync(CategoryEntity eventEntity, CancellationToken cancellationToken);

    Task DeleteByExternalIdAsync(Guid externalId, CancellationToken cancellationToken);

    Task<TResult> GetActiveIdByIdAsync<TResult>(Guid id, Expression<Func<CategoryEntity, TResult>> map, CancellationToken cancellationToken);

    Task<Guid?> GetIdByExternalIdAsync(Guid externalId, CancellationToken cancellationToken);

    Task<List<CategoryEntity>> GetListByExternalIdsAsync(List<Guid> ids, CancellationToken cancellationToken);
}