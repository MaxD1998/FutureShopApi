using Shared.Domain.Bases;
using System.Linq.Expressions;

namespace Shared.Domain.Interfaces;

public interface IBaseNoSqlRepository<TDocument> where TDocument : BaseDocument
{
    Task<long> CountAsync(CancellationToken cancellationToken);

    Task<List<TDocument>> CreateListAsync(List<TDocument> entities, CancellationToken cancellationToken);

    Task DeleteManyByIdsAsync(List<string> ids, CancellationToken cancellationToken);

    Task<TDocument> GetByIdAsync(string id, CancellationToken cancellationToken);

    Task<List<TResult>> GetListByAsync<TResult>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TResult>> map, CancellationToken cancellationToken);
}