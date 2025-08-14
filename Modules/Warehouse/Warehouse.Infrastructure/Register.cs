using Microsoft.Extensions.DependencyInjection;

namespace Warehouse.Infrastructure;

public static class Register
{
    public static void RegisterWarehouseInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<WarehouseContext>();
    }
}