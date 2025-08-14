using Product.Core;
using Product.Infrastructure;
using Shared.Api.Middlewares;

namespace Api.Modules.Product;

public static class ProductRegister
{
    public static void RegisterProductModule(this IServiceCollection services)
    {
        services.RegisterProductInfrastructure();
        services.RegisterProductCore();
        services.RegisterMiddlewares();
    }

    private static void RegisterMiddlewares(this IServiceCollection services)
    {
        services.AddScoped<PostgreSqlDbTransactionMiddleware<ProductContext>>();
    }
}