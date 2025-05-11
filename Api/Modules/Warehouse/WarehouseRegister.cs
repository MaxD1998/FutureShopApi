using Shared.Api.Middlewares;
using Warehouse.Infrastructure;

namespace Api.Modules.Warehouse;

public static class WarehouseRegister
{
    public static void RegisterWarehouseModule(this IServiceCollection services)
    {
        services.ConfigureServices();
        services.RegisterServices();
        services.RegisterMiddlewares();
    }

    private static void ConfigureServices(this IServiceCollection services)
    {
        services.AddDbContext<WarehouseContext>();
    }

    private static void RegisterMiddlewares(this IServiceCollection services)
    {
        services.AddScoped<PostgreSqlDbTransactionMiddleware<WarehouseContext>>();
    }

    private static void RegisterServices(this IServiceCollection services)
    {
    }
}