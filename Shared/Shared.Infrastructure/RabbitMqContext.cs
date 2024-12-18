using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Shared.Infrastructure.Settings;
using System.Text;
using System.Text.Json;

namespace Shared.Infrastructure;

public class RabbitMqContext
{
    private readonly string _hostName;

    public RabbitMqContext(IOptions<ConnectionSettings> connectionSettings)
    {
        _hostName = connectionSettings.Value.RabbitMQ.HostName;
    }

    public async Task SendMessageAsync(string queueName, object body)
    {
        var factory = new ConnectionFactory()
        {
            HostName = _hostName,
        };

        using (var connection = await factory.CreateConnectionAsync())
        {
            using (var channel = await connection.CreateChannelAsync())
            {
                await channel.QueueDeclareAsync(queueName, true, false, false);

                var bodyString = JsonSerializer.Serialize(body);

                await channel.BasicPublishAsync(string.Empty, queueName, Encoding.UTF8.GetBytes(bodyString));
            }
        }
    }
}