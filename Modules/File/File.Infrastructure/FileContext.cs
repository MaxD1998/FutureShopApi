using Microsoft.Extensions.Options;
using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Settings;

namespace File.Infrastructure;

public class FileContext(IOptions<ConnectionSettings> connectionSettings) : BaseNoSqlContext(connectionSettings.Value.MongoDB.File.ConnectionURI, connectionSettings.Value.MongoDB.File.DatabaseName)
{
}