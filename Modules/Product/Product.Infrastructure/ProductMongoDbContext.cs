using Microsoft.Extensions.Options;
using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Settings;

namespace Product.Infrastructure;

public class ProductMongoDbContext : BaseMongoDbContext
{
    public ProductMongoDbContext(IOptions<ConnectionSettings> connectionSettings) : base(connectionSettings.Value.MongoDB.File.ConnectionURI, connectionSettings.Value.MongoDB.File.DatabaseName)
    {
    }
}