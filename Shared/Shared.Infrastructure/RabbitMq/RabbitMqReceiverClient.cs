using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Shared.Infrastructure.RabbitMq;

public class RabbitMqReceiverClient
{
    private readonly string _hostName;

    public RabbitMqReceiverClient(string hostName)
    {
        _hostName = hostName;
    }

    public async Task RecieveMessageAsync(string exchange, string queueName, Func<string, CancellationToken, Task> action, CancellationToken cancellationToken = default)
    {
        var factory = new ConnectionFactory()
        {
            HostName = _hostName,
        };

        var connection = await factory.CreateConnectionAsync();
        var channel = await connection.CreateChannelAsync();

        await channel.ExchangeDeclareAsync(exchange, "fanout", true, true);
        await channel.QueueDeclareAsync(queueName, true, false, false);
        await channel.QueueBindAsync(queueName, exchange, "");

        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.ReceivedAsync += async (model, ea) =>
        {
            try
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    await channel.BasicNackAsync(ea.DeliveryTag, false, true);
                    return;
                }

                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                await action(message, cancellationToken);

                await channel.BasicAckAsync(ea.DeliveryTag, false);
            }
            catch
            {
                await channel.BasicNackAsync(ea.DeliveryTag, false, true);
            }
        };

        await channel.BasicConsumeAsync(queueName, false, consumer);
    }
}