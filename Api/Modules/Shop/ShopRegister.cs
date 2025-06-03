using Shared.Api.Extensions;
using Shared.Api.Middlewares;
using Shop.Core.EventHandlers;
using Shop.Core.EventServices;
using Shop.Core.Services;
using Shop.Infrastructure;
using Shop.Infrastructure.Repositories;

namespace Api.Modules.Shop;

public static class ShopRegister
{
    public static void RegisterShopModule(this IServiceCollection services)
    {
        services.ConfigureServices();
        services.RegisterRepositories();
        services.RegisterEventServices();
        services.RegisterServices();
        services.RegisterMiddlewares();

        services.AddRabbitReceiver(x =>
        {
            x.AddEventHandler<CategoryEventHandler>();
            x.AddEventHandler<ProductBaseEventHandler>();
            x.AddEventHandler<ProductEventHandler>();
        });
    }

    private static void ConfigureServices(this IServiceCollection services)
    {
        services.AddDbContext<ShopContext>();
    }

    private static void RegisterEventServices(this IServiceCollection services)
    {
        services.AddScoped<ICategoryEventService, CategoryEventService>();
        services.AddScoped<IProductBaseEventService, ProductBaseEventService>();
        services.AddScoped<IProductEventService, ProductEventService>();
    }

    private static void RegisterMiddlewares(this IServiceCollection services)
    {
        services.AddScoped<PostgreSqlDbTransactionMiddleware<ShopContext>>();
    }

    private static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAdCampaignRepository, AdCampaignRepository>();
        services.AddScoped<IBasketRepository, BasketRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductBaseRepository, ProductBaseRepository>();
        services.AddScoped<IProductParameterRepository, ProductParameterRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IPurchaseListRepository, PurchaseListRepository>();
    }

    private static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IAdCampaignService, AdCampaignService>();
        services.AddScoped<IBasketSerivce, BasketService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IProductBaseService, ProductBaseService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IPurchaseListService, PurchaseListService>();
    }
}