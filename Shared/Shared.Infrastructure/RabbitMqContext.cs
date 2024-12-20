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

    public async Task SendMessageAsync(string exchange, object body)
    {
        var factory = new ConnectionFactory()
        {
            HostName = _hostName,
        };

        using (var connection = await factory.CreateConnectionAsync())
        {
            using (var channel = await connection.CreateChannelAsync())
            {
                await channel.ExchangeDeclareAsync(exchange, "fanout", true, true);

                var bodyString = JsonSerializer.Serialize(body);

                await channel.BasicPublishAsync(exchange, string.Empty, Encoding.UTF8.GetBytes(bodyString));
            }
        }
    }
}