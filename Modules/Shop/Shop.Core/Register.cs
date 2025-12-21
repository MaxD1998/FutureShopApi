using Microsoft.Extensions.DependencyInjection;
using Shop.Core.EventServices;
using Shop.Core.Factories;
using Shop.Core.Interfaces.Services;
using Shop.Core.Services;

namespace Shop.Core;

public static class Register
{
    public static void RegisterShopCore(this IServiceCollection services)
    {
        services.AddScoped<ICategoryEventService, CategoryEventService>();
        services.AddScoped<IProductBaseEventService, ProductBaseEventService>();
        services.AddScoped<IProductEventService, ProductEventService>();
        services.AddScoped<IUserEventService, UserEventService>();

        services.AddScoped<IAdCampaignService, AdCampaignService>();
        services.AddScoped<IBasketService, BasketService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IProductBaseService, ProductBaseService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IProductReviewService, ProductReviewService>();
        services.AddScoped<IPromotionService, PromotionService>();
        services.AddScoped<IPurchaseListService, PurchaseListService>();
        services.AddScoped<IUserCompanyDetailsService, UserCompanyDetailsService>();
        services.AddScoped<IUserDeliveryAddressService, UserDeliveryAddressService>();

        services.AddScoped<ILogicFactory, LogicFactory>();
    }
}