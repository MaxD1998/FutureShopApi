using Microsoft.Extensions.DependencyInjection;
using Product.Infrastructure.Repositories;
using Product.Core.Interfaces.Repositories;

namespace Product.Infrastructure;

public static class Register
{
    public static void RegisterProductInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<ProductContext>();

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductBaseRepository, ProductBaseRepository>();
        services.AddScoped<IProductPhotoRepository, ProductPhotoRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
    }
}