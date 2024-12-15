using FluentValidation;
using Shared.Api.Middlewares;
using Shop.Core;
using Shop.Core.Services;
using Shop.Infrastructure;

namespace Api.Modules.Shop;

public static class ShopRegister
{
    public static void RegisterShopModule(this IServiceCollection services)
    {
        services.ConfigureServices();
        services.RegisterServices();
        services.RegisterMiddlewares();
    }

    private static void ConfigureServices(this IServiceCollection services)
    {
        services.AddDbContext<ShopContext>();
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(CoreAssembly).Assembly);
        });
        services.AddValidatorsFromAssembly(typeof(CoreAssembly).Assembly);
    }

    private static void RegisterMiddlewares(this IServiceCollection services)
    {
        services.AddScoped<PostgreSqlDbTransactionMiddleware<ShopContext>>();
    }

    private static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IBasketSerivce, BasketService>();
        services.AddScoped<IPurchaseListService, PurchaseListService>();
    }
}