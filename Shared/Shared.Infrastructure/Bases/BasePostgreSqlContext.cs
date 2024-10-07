using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Shared.Domain.Bases;
using Shared.Infrastructure.Errors;
using Shared.Infrastructure.Exceptions;
using Shared.Infrastructure.Settings;

namespace Shared.Infrastructure.Bases;

public abstract class BasePostgreSqlContext : DbContext
{
    protected readonly ConnectionSettings _connectionSettings;

    protected BasePostgreSqlContext(IOptions<ConnectionSettings> connectionSettings)
    {
        _connectionSettings = connectionSettings.Value;
        if (!_connectionSettings.MigrationMode && !Database.CanConnect())
            throw new ServiceUnavailableException(CommonExceptionMessage.D001DatabaseNotAvailable);
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
        optionsBuilder.UseNpgsql(ConnectionString);
    }

    protected abstract override void OnModelCreating(ModelBuilder modelBuilder);
}