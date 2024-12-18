using Shared.Core.Interfaces;

namespace Shared.Api.RabbitMq;

public sealed class RabbitMqEventRegister
{
    private static RabbitMqEventRegister _instance;
    private static List<Type> _registeredEvents = new();

    private RabbitMqEventRegister()
    {
    }

    public static IReadOnlyList<Type> RegisteredEvents => _registeredEvents;

    public static RabbitMqEventRegister Instance() => _instance ??= new();

    public void AddEventHandler<TEventHandler>() where TEventHandler : IMessageEventHandler
    {
        _registeredEvents.Add(typeof(TEventHandler));
    }
}