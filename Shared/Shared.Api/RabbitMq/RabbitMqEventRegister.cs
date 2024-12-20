using Microsoft.Extensions.DependencyInjection;
using Shared.Core.Interfaces;

namespace Shared.Api.RabbitMq;

public sealed class RabbitMqEventRegister
{
    private static RabbitMqEventRegister _instance;
    private static List<Type> _registeredEvents = new();

    private readonly IServiceCollection _services;

    private RabbitMqEventRegister(IServiceCollection services)
    {
        _services = services;
    }

    public static IReadOnlyList<Type> RegisteredEvents => _registeredEvents;

    public static RabbitMqEventRegister Instance(IServiceCollection services) => _instance ??= new(services);

    public void AddEventHandler<TEventHandler>() where TEventHandler : class, IMessageEventHandler
    {
        _services.AddScoped<TEventHandler>();
        _registeredEvents.Add(typeof(TEventHandler));
    }
}