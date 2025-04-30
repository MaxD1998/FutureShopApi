using Microsoft.EntityFrameworkCore;
using Shared.Domain.Bases;
using Shared.Domain.Interfaces;
using Shared.Infrastructure.Extensions;
using System.Linq.Expressions;

namespace Shared.Infrastructure.Bases;

public interface IBaseRepository
{
    Task<T> CreateAsync<T>(T entity, CancellationToken cancellationToken) where T : BaseEntity;

    Task DeleteByIdAsync<T>(Guid id, CancellationToken cancellationToken) where T : BaseEntity;

    Task<TResult> GetByIdAsync<TEntity, TResult>(Guid id, Expression<Func<TEntity, TResult>> map, CancellationToken cancellationToken) where TEntity : BaseEntity;

    Task<List<TResult>> GetListAsync<TEntity, TResult>(Expression<Func<TEntity, TResult>> map, CancellationToken cancellationToken) where TEntity : BaseEntity;

    Task<PageDto<TResult>> GetPageAsync<TEntity, TResult>(int pageNumber, Expression<Func<TEntity, TResult>> map, CancellationToken cancellationToken) where TEntity : BaseEntity;

    Task<T> UpdateAsync<T>(Guid id, T entity, CancellationToken cancellationToken) where T : BaseEntity, IUpdate<T>;
}

public abstract class BaseRepository<TContext>(TContext context) : IBaseRepository where TContext : BasePostgreSqlContext
{
    protected readonly TContext _context = context;

    public virtual async Task<T> CreateAsync<T>(T entity, CancellationToken cancellationToken) where T : BaseEntity
    {
        await _context.Set<T>().AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public Task DeleteByIdAsync<T>(Guid id, CancellationToken cancellationToken) where T : BaseEntity
        => _context.Set<T>().Where(x => x.Id == id).ExecuteDeleteAsync(cancellationToken);

    public Task<TResult> GetByIdAsync<TEntity, TResult>(Guid id, Expression<Func<TEntity, TResult>> map, CancellationToken cancellationToken) where TEntity : BaseEntity
                => _context.Set<TEntity>().Where(x => x.Id == id).Select(map).FirstOrDefaultAsync();

    public Task<List<TResult>> GetListAsync<TEntity, TResult>(Expression<Func<TEntity, TResult>> map, CancellationToken cancellationToken) where TEntity : BaseEntity
        => _context.Set<TEntity>().Select(map).ToListAsync();

    public Task<PageDto<TResult>> GetPageAsync<TEntity, TResult>(int pageNumber, Expression<Func<TEntity, TResult>> map, CancellationToken cancellationToken) where TEntity : BaseEntity
        => _context.Set<TEntity>().Select(map).ToPageAsync(pageNumber, cancellationToken);

    public virtual async Task<T> UpdateAsync<T>(Guid id, T entity, CancellationToken cancellationToken) where T : BaseEntity, IUpdate<T>
    {
        var entityToUpdate = await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        entityToUpdate.Update(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entityToUpdate;
    }

    protected Task<bool> AnyAsync<T>(Expression<Func<T, bool>> condition, CancellationToken cancellationToken) where T : BaseEntity
        => _context.Set<T>().AnyAsync(condition, cancellationToken);

    protected Task<T> GetByAsync<T>(Expression<Func<T, bool>> condition, CancellationToken cancellationToken) where T : BaseEntity
        => _context.Set<T>().AsNoTracking().Where(condition).FirstOrDefaultAsync(cancellationToken);
}