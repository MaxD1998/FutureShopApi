using MongoDB.Driver;
using Shared.Domain.Bases;
using Shared.Infrastructure.Errors;
using Shared.Infrastructure.Exceptions;

namespace Shared.Infrastructure.Bases;

public abstract class BaseNoSqlContext
{
    protected BaseNoSqlContext(string connectionString, string dbName)
    {
        try
        {
            Client = new MongoClient(connectionString);
            Database = Client.GetDatabase(dbName);
        }
        catch
        {
            throw new ServiceUnavailableException(CommonExceptionMessage.D001DatabaseNotAvailable);
        }
    }

    public IMongoClient Client { get; private set; }

    public IMongoDatabase Database { get; private set; }

    public async Task AddAsync<TDocument>(TDocument document, CancellationToken cancellationToken = default) where TDocument : BaseDocument
    {
        document.CreateTime = DateTime.UtcNow;
        await Set<TDocument>().InsertOneAsync(document, null, cancellationToken);
    }

    public async Task AddRangeAsync<TDocument>(IEnumerable<TDocument> documents, CancellationToken cancellationToken = default) where TDocument : BaseDocument
    {
        var date = DateTime.UtcNow;

        foreach (var document in documents)
            document.CreateTime = date;

        await Set<TDocument>().InsertManyAsync(documents, null, cancellationToken);
    }

    public async Task<List<TDocument>> GetAllAsync<TDocument>(CancellationToken cancellationToken = default) where TDocument : BaseDocument
    {
        var filter = FilterDefinition<TDocument>.Empty;
        return await Set<TDocument>().Find(filter).ToListAsync(cancellationToken);
    }

    public IMongoCollection<TDocument> Set<TDocument>() where TDocument : BaseDocument
        => Database.GetCollection<TDocument>(typeof(TDocument).Name.Replace("Document", string.Empty));
}