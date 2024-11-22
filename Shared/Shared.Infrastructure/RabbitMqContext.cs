using Microsoft.Extensions.Options;
using Shared.Infrastructure.RabbitMq;
using Shared.Infrastructure.Settings;

namespace Shared.Infrastructure;

public class RabbitMqContext : IDisposable
{
    public RabbitMqContext(IOptions<ConnectionSettings> connectionSettings)
    {
        var hostName = connectionSettings.Value.RabbitMQ.HostName;

        Receiver = new RabbitMqReceiverClient(hostName);
        Sender = new RabbitMqSenderClient(hostName);
    }

    public RabbitMqReceiverClient Receiver { get; init; }

    public RabbitMqSenderClient Sender { get; init; }

    public void Dispose() => Receiver.Dispose();
}