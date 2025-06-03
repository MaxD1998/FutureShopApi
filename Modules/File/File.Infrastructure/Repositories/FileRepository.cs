using File.Domain.Documents;
using MongoDB.Driver;
using Shared.Infrastructure.Bases;

namespace File.Infrastructure.Repositories;

public interface IFileRepository : IBaseNoSqlRepository<FileDocument>
{
    Task<List<string>> Get1000IdAsync(int pageIndex, CancellationToken cancellationToken);
}

public class FileRepository(FileContext context) : BaseNoSqlRepository<FileContext, FileDocument>(context), IFileRepository
{
    public Task<List<string>> Get1000IdAsync(int pageIndex, CancellationToken cancellationToken)
    {
        var pageSize = 1000;
        return _context.Set<FileDocument>().Find(FilterDefinition<FileDocument>.Empty).Skip(pageIndex * pageSize).Limit(pageSize).Project(x => x.Id).ToListAsync(cancellationToken);
    }
}