using Microsoft.EntityFrameworkCore;
using Shared.Domain.Bases;
using System.Linq.Expressions;

namespace Shared.Infrastructure.Bases;

public abstract class BaseRepository<TContext>(TContext context) where TContext : BasePostgreSqlContext
{
    protected readonly TContext _context = context;

    protected async Task<T> AddAsync<T>(T entity, CancellationToken cancellationToken) where T : BaseEntity
    {
        await _context.Set<T>().AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }

    protected Task<bool> AnyAsync<T>(Expression<Func<T, bool>> condition, CancellationToken cancellationToken) where T : BaseEntity
            => _context.Set<T>().AnyAsync(condition, cancellationToken);

    protected Task DeleteByAsync<T>(Expression<Func<T, bool>> condition, CancellationToken cancellationToken) where T : BaseEntity
        => _context.Set<T>().Where(condition).ExecuteDeleteAsync(cancellationToken);

    protected Task<T> GetByAsync<T>(Expression<Func<T, bool>> condition, CancellationToken cancellationToken) where T : BaseEntity
        => _context.Set<T>().AsNoTracking().Where(condition).FirstOrDefaultAsync(cancellationToken);
}