using FluentValidation;
using Product.Core;
using Product.Core.Interfaces.Services;
using Product.Core.Services;
using Product.Infrastructure;
using Shared.Api.Middlewares;

namespace Api.Modules.Product;

public static class ProductRegister
{
    public static void RegisterProductModule(this IServiceCollection services)
    {
        services.ConfigureServices();
        services.RegisterServices();
        services.RegisterMiddlewares();

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

    private static void RegisterMiddlewares(this IServiceCollection services)
    {
        services.AddScoped<DbTransactionMiddleware<ProductContext>>();
    }

    private static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IHeaderService, HeaderService>();
    }
}