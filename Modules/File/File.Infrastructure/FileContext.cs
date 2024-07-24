using Microsoft.Extensions.Options;
using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Settings;

namespace File.Infrastructure;

public class FileContext : BaseMongoDbContext
{
    public FileContext(IOptions<ConnectionSettings> connectionSettings) : base(connectionSettings.Value.MongoDB.File.ConnectionURI, connectionSettings.Value.MongoDB.File.DatabaseName)
    {
    }
}