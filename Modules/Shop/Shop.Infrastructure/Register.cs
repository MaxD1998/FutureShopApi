using Microsoft.Extensions.DependencyInjection;
using Shop.Infrastructure.Repositories;

namespace Shop.Infrastructure;

public static class Register
{
    public static void RegisterShopInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<ShopContext>();

        services.AddScoped<IAdCampaignRepository, AdCampaignRepository>();
        services.AddScoped<IBasketRepository, BasketRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductBaseRepository, ProductBaseRepository>();
        services.AddScoped<IProductParameterRepository, ProductParameterRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IPurchaseListRepository, PurchaseListRepository>();
    }
}