using Shared.Api.Middlewares;
using Warehouse.Core;
using Warehouse.Infrastructure;

namespace Api.Modules.Warehouse;

public static class WarehouseRegister
{
    public static void RegisterWarehouseModule(this IServiceCollection services)
    {
        services.RegisterWarehouseInfrastructure();
        services.RegisterWarehouseCore();
        services.RegisterMiddlewares();
    }

    private static void RegisterMiddlewares(this IServiceCollection services)
    {
        services.AddScoped<PostgreSqlDbTransactionMiddleware<WarehouseContext>>();
    }
}