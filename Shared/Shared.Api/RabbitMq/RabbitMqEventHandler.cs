using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Core.Interfaces;
using Shared.Infrastructure;

namespace Shared.Api.RabbitMq;

public class RabbitMqEventHandler : BackgroundService
{
    private readonly RabbitMqContext _rabbitMqContext;
    private readonly RabbitMqEventRegister _rabbitMqEventRegister;
    private readonly IServiceProvider _serviceProvider;

    public RabbitMqEventHandler(IServiceProvider serviceProvider, RabbitMqContext rabbitMqContext)
    {
        _serviceProvider = serviceProvider;
        _rabbitMqContext = rabbitMqContext;
    }

    public delegate void MessageEventHandler();

    public event MessageEventHandler Recieved;

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
            await _rabbitMqContext.Receiver.RecieveMessageAsync(eventHandler.QueueName, eventHandler.ExecuteAsync, stoppingToken);
        }
    }
}