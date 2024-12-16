using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Settings;
using System.Reflection;

namespace Shop.Infrastructure;

public class ShopContext : BasePostgreSqlContext
{
    public ShopContext(IOptions<ConnectionSettings> connectionSettings) : base(connectionSettings)
    {
    }

    protected override string ConnectionString => _connectionSettings.PostgreSQL.ShopDbCs;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}