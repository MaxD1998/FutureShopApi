using Microsoft.EntityFrameworkCore;
using Shared.Domain.Bases;
using Shared.Domain.Interfaces;
using Shared.Infrastructure.Extensions;
using System.Linq.Expressions;

namespace Shared.Infrastructure.Bases;

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

    Task<PageDto<TResult>> GetPageAsync<TResult>(int pageNumber, Expression<Func<TEntity, TResult>> map, CancellationToken cancellationToken);
}

public abstract class BaseRepository<TContext, TEntity>(TContext context) : IBaseRepository<TEntity> where TContext : BaseContext where TEntity : BaseEntity, IUpdate<TEntity>
{
    protected readonly TContext _context = context;

    public virtual async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _context.Set<TEntity>().AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public virtual Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
        => _context.Set<TEntity>().Where(x => x.Id == id).ExecuteDeleteAsync(cancellationToken);

    public virtual Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _context.Set<TEntity>().AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();

    public virtual Task<TResult> GetByIdAsync<TResult>(Guid id, Expression<Func<TEntity, TResult>> map, CancellationToken cancellationToken)
        => _context.Set<TEntity>().AsNoTracking().Where(x => x.Id == id).Select(map).FirstOrDefaultAsync();

    public virtual Task<List<TResult>> GetListAsync<TResult>(Expression<Func<TEntity, TResult>> map, CancellationToken cancellationToken)
        => _context.Set<TEntity>().AsNoTracking().Select(map).ToListAsync();

    public virtual Task<List<TResult>> GetListAsync<TResult>(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, TResult>> map, CancellationToken cancellationToken)
        => _context.Set<TEntity>().AsNoTracking().Where(condition).Select(map).ToListAsync();

    public virtual Task<PageDto<TResult>> GetPageAsync<TResult>(int pageNumber, Expression<Func<TEntity, TResult>> map, CancellationToken cancellationToken)
        => _context.Set<TEntity>().AsNoTracking().Select(map).ToPageAsync(pageNumber, cancellationToken);

    protected Task<bool> AnyAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken)
        => _context.Set<TEntity>().AnyAsync(condition, cancellationToken);

    protected virtual Task DeleteByAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken)
        => _context.Set<TEntity>().Where(condition).ExecuteDeleteAsync(cancellationToken);

    protected virtual Task<TEntity> GetByAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken)
        => _context.Set<TEntity>().AsNoTracking().Where(condition).FirstOrDefaultAsync(cancellationToken);
}