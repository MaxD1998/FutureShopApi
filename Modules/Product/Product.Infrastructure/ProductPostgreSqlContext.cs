using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Settings;
using System.Reflection;

namespace Product.Infrastructure;

public class ProductPostgreSqlContext : BasePostgreSqlContext
{
    public ProductPostgreSqlContext(IOptions<ConnectionSettings> connectionSettings) : base(connectionSettings)
    {
    }

    protected override string ConnectionString => _connectionSettings.PostgreSQL.ProductDbCs;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}