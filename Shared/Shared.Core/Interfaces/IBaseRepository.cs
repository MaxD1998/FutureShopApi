using Shared.Domain.Bases;
using Shared.Shared.Dtos;
using System.Linq.Expressions;

namespace Shared.Core.Interfaces;

public interface IBaseRepository
{
    Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
}

public interface IBaseRepository<TEntity> : IBaseRepository where TEntity : BaseEntity
{
    Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken);

    Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<TResult> GetByIdAsync<TResult>(Guid id, Expression<Func<TEntity, TResult>> map, CancellationToken cancellationToken);

    Task<List<TResult>> GetListAsync<TResult>(Expression<Func<TEntity, TResult>> map, CancellationToken cancellationToken);

    Task<List<TResult>> GetListAsync<TResult>(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, TResult>> map, CancellationToken cancellationToken);

    Task<PageDto<TResult>> GetPageAsync<TResult>(PaginationDto pagination, Expression<Func<TEntity, TResult>> map, CancellationToken cancellationToken);

    Task<PageDto<TResult>> GetPageAsync<TResult>(PaginationDto pagination, Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, TResult>> map, CancellationToken cancellationToken);
}