using Microsoft.Extensions.DependencyInjection;
using Product.Core.EventServices;
using Product.Core.Services;

namespace Product.Core;

public static class Register
{
    public static void RegisterProductCore(this IServiceCollection services)
    {
        services.AddScoped<IProductPhotoEventService, ProductPhotoEventService>();

        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IProductBaseService, ProductBaseService>();
        services.AddScoped<IProductService, ProductService>();
    }
}