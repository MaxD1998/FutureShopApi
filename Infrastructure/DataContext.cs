using Microsoft.EntityFrameworkCore;
using Shared.Bases;
using System.Reflection;

namespace Infrastructure;

public class DataContext : DbContext
{
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}