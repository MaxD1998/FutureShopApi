namespace Shared.Infrastructure.Settings;

public class ConnectionSettings
{
    public bool MigrationMode { get; set; }

    public MongoDBSettings MongoDB { get; init; }

    public PostgreSQLSettings PostgreSQL { get; init; }

    public RabbitMQSettings RabbitMQ { get; init; }
}