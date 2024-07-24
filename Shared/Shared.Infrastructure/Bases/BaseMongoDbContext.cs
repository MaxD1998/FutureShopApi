using MongoDB.Driver;
using Shared.Infrastructure.Errors;
using Shared.Infrastructure.Exceptions;

namespace Shared.Infrastructure.Bases;

public abstract class BaseMongoDbContext
{
    protected BaseMongoDbContext(string connectionString, string dbName)
    {
        try
        {
            Client = new MongoClient(connectionString);
            Database = Client.GetDatabase(dbName);
        }
        catch
        {
            throw new ServiceUnavailableException(ExceptionMessage.D001DatabaseNotAvailable);
        }
    }

    public IMongoClient Client { get; private set; }

    public IMongoDatabase Database { get; private set; }

    public async Task AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
    {
        var collection = Database.GetCollection<TEntity>(typeof(TEntity).Name.Replace("Entity", string.Empty));
        await collection.InsertOneAsync(entity, null, cancellationToken);
    }

    public async Task AddRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        var collection = Database.GetCollection<TEntity>(typeof(TEntity).Name.Replace("Entity", string.Empty));
        await collection.InsertManyAsync(entities, null, cancellationToken);
    }

    public IQueryable<TEntity> Set<TEntity>()
    {
        var collection = Database.GetCollection<TEntity>(typeof(TEntity).Name.Replace("Entity", string.Empty));
        return collection.AsQueryable();
    }
}