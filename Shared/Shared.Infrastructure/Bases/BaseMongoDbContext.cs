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
        => await Set<TEntity>().InsertOneAsync(entity, null, cancellationToken);

    public async Task AddRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        => await Set<TEntity>().InsertManyAsync(entities, null, cancellationToken);

    public async Task<List<TEntity>> GetAllAsync<TEntity>(CancellationToken cancellationToken = default)
    {
        var filter = FilterDefinition<TEntity>.Empty;
        return await Set<TEntity>().Find(filter).ToListAsync(cancellationToken);
    }

    public IMongoCollection<TEntity> Set<TEntity>()
        => Database.GetCollection<TEntity>(typeof(TEntity).Name.Replace("Entity", string.Empty));
}