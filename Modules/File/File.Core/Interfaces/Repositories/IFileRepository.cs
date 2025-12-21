using File.Domain.Documents;
using Shared.Domain.Interfaces;

namespace File.Core.Interfaces.Repositories;

public interface IFileRepository : IBaseNoSqlRepository<FileDocument>
{
    Task<List<string>> Get1000IdAsync(int pageIndex, CancellationToken cancellationToken);
}