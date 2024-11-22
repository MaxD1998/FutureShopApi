using Microsoft.Extensions.DependencyInjection;
using Shared.Core.Interfaces;

namespace Shared.Api.RabbitMq;

public sealed class RabbitMqEventRegister
{
    private static RabbitMqEventRegister _instance;
    private static List<Type> _registeredEvents = new();
    private static IServiceProvider _serviceProvider;

    private RabbitMqEventRegister(IServiceCollection services)
    {
        _serviceProvider = services.BuildServiceProvider();
    }

    public static IReadOnlyList<Type> RegisteredEvents => _registeredEvents;

    public static RabbitMqEventRegister Instance(IServiceCollection services) => _instance ??= new(services);

    public void AddEventHandler<TEventHandler>() where TEventHandler : IMessageEventHandler
    {
        _registeredEvents.Add(typeof(TEventHandler));
    }
}