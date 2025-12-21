using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shared.Domain.Bases;
using Shared.Infrastructure.Exceptions;
using Shared.Shared.Settings;
using System.Diagnostics;

namespace Shared.Infrastructure.Bases;

public abstract class BaseContext : DbContext
{
    protected readonly ConnectionSettings _connectionSettings;

    protected BaseContext(IOptions<ConnectionSettings> connectionSettings)
    {
        _connectionSettings = connectionSettings.Value;
        if (!_connectionSettings.MigrationMode && !Database.CanConnect())
            throw new ServiceUnavailableException();
    }

    protected abstract string ConnectionString { get; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entities = ChangeTracker
            .Entries<BaseEntity>()
            .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);

        foreach (var entity in entities)
        {
            switch (entity.State)
            {
                case EntityState.Added:
                    entity.Entity.CreateTime = DateTime.UtcNow;
                    break;

                case EntityState.Modified:
                    entity.Entity.ModifyTime = DateTime.UtcNow;
                    entity.Property(x => x.CreateTime).IsModified = false;
                    break;

                default:
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(ConnectionString)
            .LogTo(message => Debug.WriteLine(message), LogLevel.Information);
    }

    protected abstract override void OnModelCreating(ModelBuilder modelBuilder);
}