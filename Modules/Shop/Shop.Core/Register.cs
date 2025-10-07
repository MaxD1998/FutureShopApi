using Microsoft.Extensions.DependencyInjection;
using Shop.Core.EventServices;
using Shop.Core.Factories;
using Shop.Core.Services;

namespace Shop.Core;

public static class Register
{
    public static void RegisterShopCore(this IServiceCollection services)
    {
        services.AddScoped<ICategoryEventService, CategoryEventService>();
        services.AddScoped<IProductBaseEventService, ProductBaseEventService>();
        services.AddScoped<IProductEventService, ProductEventService>();

        services.AddScoped<IAdCampaignService, AdCampaignService>();
        services.AddScoped<IBasketSerivce, BasketService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IProductBaseService, ProductBaseService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IPromotionService, PromotionService>();
        services.AddScoped<IPurchaseListService, PurchaseListService>();

        services.AddScoped<ILogicFactory, LogicFactory>();
    }
}