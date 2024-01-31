using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Shared.Domain.Bases;
using Shared.Infrastructure.Settings;

namespace Shared.Infrastructure.Bases;

public abstract class BaseContext : DbContext
{
    protected readonly ConnectionSettings _connectionSettings;

    public BaseContext(IOptions<ConnectionSettings> connectionSettings)
    {
        _connectionSettings = connectionSettings.Value;
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