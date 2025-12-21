using Microsoft.EntityFrameworkCore;
using Shared.Core.Interfaces;
using Shared.Domain.Bases;
using Shared.Domain.Interfaces;
using Shared.Infrastructure.Extensions;
using Shared.Shared.Dtos;
using System.Linq.Expressions;

namespace Shared.Infrastructure.Bases;

public abstract class BaseRepository<TContext, TEntity>(TContext context) : IBaseRepository<TEntity> where TContext : BaseContext where TEntity : BaseEntity, IUpdate<TEntity>
{
    protected readonly TContext _context = context;

    public virtual async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        if (entity is IEntityValidation entityValidation)
            entityValidation.Validate();

        await _context.Set<TEntity>().AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
        => _context.Set<TEntity>().Where(x => x.Id == id).ExecuteDeleteAsync(cancellationToken);

    public Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _context.Set<TEntity>().AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);

    public Task<TResult> GetByIdAsync<TResult>(Guid id, Expression<Func<TEntity, TResult>> map, CancellationToken cancellationToken)
        => _context.Set<TEntity>().AsNoTracking().Where(x => x.Id == id).Select(map).FirstOrDefaultAsync(cancellationToken);

    public Task<List<TResult>> GetListAsync<TResult>(Expression<Func<TEntity, TResult>> map, CancellationToken cancellationToken)
        => _context.Set<TEntity>().AsNoTracking().Select(map).ToListAsync(cancellationToken);

    public Task<List<TResult>> GetListAsync<TResult>(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, TResult>> map, CancellationToken cancellationToken)
        => _context.Set<TEntity>().AsNoTracking().Where(condition).Select(map).ToListAsync(cancellationToken);

    public Task<PageDto<TResult>> GetPageAsync<TResult>(PaginationDto pagination, Expression<Func<TEntity, TResult>> map, CancellationToken cancellationToken)
        => _context.Set<TEntity>().AsNoTracking().Select(map).ToPageAsync(pagination, cancellationToken);

    public Task<PageDto<TResult>> GetPageAsync<TResult>(PaginationDto pagination, Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, TResult>> map, CancellationToken cancellationToken)
        => _context.Set<TEntity>().AsNoTracking().Where(condition).Select(map).ToPageAsync(pagination, cancellationToken);

    protected Task<bool> AnyAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken)
        => _context.Set<TEntity>().AnyAsync(condition, cancellationToken);

    protected Task DeleteByAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken)
        => _context.Set<TEntity>().Where(condition).ExecuteDeleteAsync(cancellationToken);

    protected Task<TEntity> GetByAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken)
        => _context.Set<TEntity>().AsNoTracking().Where(condition).FirstOrDefaultAsync(cancellationToken);
}