using File.Domain.Documents;
using Shared.Infrastructure.Bases;

namespace File.Infrastructure.Repositories;

public interface IFileRepository : IBaseNoSqlRepository<FileDocument>
{
}

public class FileRepository(FileContext context) : BaseNoSqlRepository<FileContext, FileDocument>(context), IFileRepository
{
}