using Core;
using FluentValidation;
using Infrastructure;
using Shared.Dtos.Settings;

namespace Api.Extensions;

public static class ServiceExtension
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddNpgsql<DataContext>(configuration.GetConnectionString(nameof(ConnectionSettings.DbConnectionString)));
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(CoreAssembly).Assembly);
        });
        services.AddValidatorsFromAssembly(typeof(CoreAssembly).Assembly);
        services.AddAutoMapper(typeof(CoreAssembly).Assembly);
    }
}