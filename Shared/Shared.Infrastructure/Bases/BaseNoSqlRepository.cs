using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Shared.Infrastructure.Bases;

public interface IBaseNoSqlRepository<TDocument> where TDocument : BaseDocument
{
    Task<long> CountAsync(CancellationToken cancellationToken);

    Task<List<TDocument>> CreateListAsync(List<TDocument> entities, CancellationToken cancellationToken);

    Task DeleteManyByIdsAsync(List<string> ids, CancellationToken cancellationToken);

    Task<TDocument> GetByIdAsync(string id, CancellationToken cancellationToken);

    Task<List<TResult>> GetListByAsync<TResult>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TResult>> map, CancellationToken cancellationToken);
}

public class BaseNoSqlRepository<TContext, TDocument>(TContext context) : IBaseNoSqlRepository<TDocument> where TContext : BaseNoSqlContext where TDocument : BaseDocument
{
    protected readonly TContext _context = context;

    public Task<long> CountAsync(CancellationToken cancellationToken)
        => _context.Set<TDocument>().CountDocumentsAsync(FilterDefinition<TDocument>.Empty, null, cancellationToken);

    public async Task<List<TDocument>> CreateListAsync(List<TDocument> entities, CancellationToken cancellationToken)
    {
        await _context.AddRangeAsync(entities, cancellationToken);

        return entities;
    }

    public Task DeleteManyByIdsAsync(List<string> ids, CancellationToken cancellationToken)
        => _context.Set<TDocument>().DeleteManyAsync(x => ids.Contains(x.Id), cancellationToken);

    public Task<TDocument> GetByIdAsync(string id, CancellationToken cancellationToken)
        => _context.Set<TDocument>().Find(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);

    public Task<List<TResult>> GetListByAsync<TResult>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TResult>> map, CancellationToken cancellationToken)
        => _context.Set<TDocument>().Find(filter).Project(map).ToListAsync(cancellationToken);
}