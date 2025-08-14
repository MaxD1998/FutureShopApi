﻿using Shared.Api.Extensions;
using Shared.Api.Middlewares;
using Shop.Core;
using Shop.Core.EventHandlers;
using Shop.Infrastructure;

namespace Api.Modules.Shop;

public static class ShopRegister
{
    public static void RegisterShopModule(this IServiceCollection services)
    {
        services.RegisterShopInfrastructure();
        services.RegisterShopCore();
        services.RegisterMiddlewares();

        services.AddRabbitReceiver(x =>
        {
            x.AddEventHandler<CategoryEventHandler>();
            x.AddEventHandler<ProductBaseEventHandler>();
            x.AddEventHandler<ProductEventHandler>();
        });
    }

    private static void RegisterMiddlewares(this IServiceCollection services)
    {
        services.AddScoped<PostgreSqlDbTransactionMiddleware<ShopContext>>();
    }
}