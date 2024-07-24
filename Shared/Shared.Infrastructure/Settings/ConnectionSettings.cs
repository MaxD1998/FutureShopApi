namespace Shared.Infrastructure.Settings;

public class ConnectionSettings
{
    public MongoDBSettings MongoDB { get; init; }

    public PostgreSQLSettings PostgreSQL { get; init; }
}