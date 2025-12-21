using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Shared.Core.Interfaces;
using Shared.Infrastructure.RabbitMq;
using Shared.Shared.Settings;

namespace Shared.Api.RabbitMq;

public class RabbitMqEventHandler : BackgroundService
{
    private readonly RabbitMqReceiverClient _receiver;
    private readonly IServiceProvider _serviceProvider;

    public RabbitMqEventHandler(IServiceProvider serviceProvider, IOptions<ConnectionSettings> connectionSettings)
    {
        _serviceProvider = serviceProvider;
        _receiver = new RabbitMqReceiverClient(connectionSettings.Value.RabbitMQ.HostName);
    }

    public delegate void MessageEventHandler();

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var scopedServiceProvider = _serviceProvider.CreateScope();
        foreach (var registeredEvent in RabbitMqEventRegister.RegisteredEvents)
        {
            var constructors = registeredEvent.GetConstructors();

            if (constructors.Length > 1)
                throw new Exception();

            var constructor = constructors.FirstOrDefault();
            var constructorParameters = constructor.GetParameters().Select(x => x.ParameterType);
            var parameterInstances = constructorParameters
                .Select(scopedServiceProvider.ServiceProvider.GetService)
                .ToArray();

            var eventHandler = constructor.Invoke(parameterInstances) as IMessageEventHandler;
            await _receiver.RecieveMessageAsync(eventHandler.Exchange, eventHandler.QueueName, eventHandler.ExecuteAsync, stoppingToken);
        }
    }
}