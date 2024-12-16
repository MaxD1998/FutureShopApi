using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Settings;
using System.Reflection;

namespace Warehouse.Infrastructure;

public class WarehouseContext : BasePostgreSqlContext
{
    public WarehouseContext(IOptions<ConnectionSettings> connectionSettings) : base(connectionSettings)
    {
    }

    protected override string ConnectionString => _connectionSettings.PostgreSQL.WarehouseDbCs;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}