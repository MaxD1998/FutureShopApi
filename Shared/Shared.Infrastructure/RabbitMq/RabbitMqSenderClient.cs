using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Shared.Infrastructure.RabbitMq;

public class RabbitMqSenderClient(string hostName)
{
    private readonly string _hostName = hostName;

    public void SendMessage(string queueName, object body)
    {
        var factory = new ConnectionFactory()
        {
            HostName = _hostName,
        };

        using (var connection = factory.CreateConnection())
        {
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queueName, true, false, false);

                var bodyString = JsonSerializer.Serialize(body);

                channel.BasicPublish(string.Empty, queueName, null, Encoding.UTF8.GetBytes(bodyString));
            }
        }
    }
}