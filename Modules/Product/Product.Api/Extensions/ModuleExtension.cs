using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Product.Core;
using Product.Infrastructure;

namespace Authorization.Api.Extensions;

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
        services.AddAutoMapper(typeof(CoreAssembly).Assembly);
    }

    private static void RegisterServices(this IServiceCollection services)
    {
    }
}