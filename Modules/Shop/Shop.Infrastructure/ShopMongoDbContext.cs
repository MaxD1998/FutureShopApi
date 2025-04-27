using Microsoft.Extensions.Options;
using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Settings;

namespace Shop.Infrastructure;

public class ShopMongoDbContext : BaseMongoDbContext
{
    public ShopMongoDbContext(IOptions<ConnectionSettings> connectionSettings) : base(connectionSettings.Value.MongoDB.Shop.ConnectionURI, connectionSettings.Value.MongoDB.Shop.DatabaseName)
    {
    }
}