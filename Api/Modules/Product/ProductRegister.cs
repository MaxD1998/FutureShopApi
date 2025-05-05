using Product.Core.Services;
using Product.Infrastructure;
using Product.Infrastructure.Repositories;
using Shared.Api.Middlewares;

namespace Api.Modules.Product;

public static class ProductRegister
{
    public static void RegisterProductModule(this IServiceCollection services)
    {
        services.ConfigureServices();
        services.RegisterRepositories();
        services.RegisterServices();
        services.RegisterMiddlewares();
    }

    private static void ConfigureServices(this IServiceCollection services)
    {
        services.AddDbContext<ProductContext>();
    }

    private static void RegisterMiddlewares(this IServiceCollection services)
    {
        services.AddScoped<PostgreSqlDbTransactionMiddleware<ProductContext>>();
    }

    private static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductBaseRepository, ProductBaseRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
    }

    private static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IProductBaseService, ProductBaseService>();
        services.AddScoped<IProductService, ProductService>();
    }
}