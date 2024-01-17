using Core;
using FluentValidation;

namespace Api.Extensions;

public static class ServiceExtension
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(CoreAssembly).Assembly);
        });
        services.AddValidatorsFromAssembly(typeof(CoreAssembly).Assembly);
        services.AddAutoMapper(typeof(CoreAssembly).Assembly);
    }
}