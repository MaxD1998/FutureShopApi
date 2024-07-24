using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Settings;
using System.Reflection;

namespace Authorization.Inrfrastructure;

public class AuthContext : BasePostgreSqlContext
{
    public AuthContext(IOptions<ConnectionSettings> connectionSettings) : base(connectionSettings)
    {
    }

    protected override string ConnectionString => _connectionSettings.PostgreSQL.AuthorizationDbCs;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}