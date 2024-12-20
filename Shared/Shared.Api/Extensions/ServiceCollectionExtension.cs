using Microsoft.Extensions.DependencyInjection;
using Shared.Api.RabbitMq;

namespace Shared.Api.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddRabbitReceiver(this IServiceCollection services, Action<RabbitMqEventRegister> action = null)
    {
        if (action != null)
            action(RabbitMqEventRegister.Instance(services));

        services.AddHostedService<RabbitMqEventHandler>();
    }
}