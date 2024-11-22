using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Shared.Infrastructure.RabbitMq;

public class RabbitMqReceiverClient : IDisposable
{
    private readonly IModel _channel;
    private readonly IConnection _connection;

    public RabbitMqReceiverClient(string hostName)
    {
        var factory = new ConnectionFactory()
        {
            HostName = hostName,
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
    }

    public void Dispose()
    {
        _channel.Dispose();
        _connection.Dispose();
    }

    public async Task RecieveMessageAsync(string queueName, Func<string, CancellationToken, Task> action, CancellationToken cancellationToken = default)
    {
        _channel.QueueDeclare(queueName, true, false, false);

        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += async (model, ea) =>
        {
            try
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    _channel.BasicNack(ea.DeliveryTag, false, true);
                    return;
                }

                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                await action(message, cancellationToken);

                _channel.BasicAck(ea.DeliveryTag, false);
            }
            catch
            {
                _channel.BasicNack(ea.DeliveryTag, false, true);
            }
        };

        _channel.BasicConsume(queueName, false, consumer);

        await Task.CompletedTask;
    }
}