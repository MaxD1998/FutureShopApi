using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Shared.Infrastructure.Bases;
using Shared.Shared.Settings;
using System.Reflection;

namespace Authorization.Infrastructure;

public class AuthContext : BaseContext
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