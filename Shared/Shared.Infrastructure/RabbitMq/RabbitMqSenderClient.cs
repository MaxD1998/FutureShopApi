using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Shared.Infrastructure.RabbitMq;

public class RabbitMqSenderClient(string hostName)
{
    private readonly string _hostName = hostName;

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