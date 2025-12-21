using Product.Domain.Entities;
using Shared.Core.Interfaces;
using System.Linq.Expressions;

namespace Product.Core.Interfaces.Repositories;

public interface ICategoryRepository : IBaseRepository<CategoryEntity>, IUpdateRepository<CategoryEntity>
{
    Task<List<CategoryEntity>> GetListByIds(List<Guid> ids, CancellationToken cancellationToken);

    Task<List<TResult>> GetListPotentialParentCategories<TResult>(Guid? id, List<Guid> childIds, Expression<Func<CategoryEntity, TResult>> map, CancellationToken cancellationToken);

    Task<List<TResult>> GetListPotentialSubcategoriesAsync<TResult>(Guid? id, Guid? parentId, List<Guid> childIds, Expression<Func<CategoryEntity, TResult>> map, CancellationToken cancellationToken);
}