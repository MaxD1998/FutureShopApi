using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Product.Core;
using Product.Core.Interfaces.Services;
using Product.Core.Services;
using Product.Infrastructure;

namespace Product.Api.Extensions;

public static class ModuleExtension
{
    public static void RegisterProductModule(this IServiceCollection services)
    {
        services.ConfigureServices();
        services.RegisterServices();
    }

    private static void ConfigureServices(this IServiceCollection services)
    {
        services.AddDbContext<ProductContext>();
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(CoreAssembly).Assembly);
        });
        services.AddValidatorsFromAssembly(typeof(CoreAssembly).Assembly);
    }

    private static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IHeaderService, HeaderService>();
    }
}