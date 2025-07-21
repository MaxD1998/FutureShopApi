﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shared.Domain.Bases;
using Shared.Infrastructure.Exceptions;
using Shared.Infrastructure.Settings;
using System.Diagnostics;

namespace Shared.Infrastructure.Bases;

public abstract class BaseContext : DbContext
{
    protected readonly ConnectionSettings _connectionSettings;

    protected BaseContext(IOptions<ConnectionSettings> connectionSettings)
    {
        _connectionSettings = connectionSettings.Value;
        if (!_connectionSettings.MigrationMode && !Database.CanConnect())
            throw new DatabaseUnavailableException();
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
                    entity.Entity.MarkCreated();
                    break;

                case EntityState.Modified:
                    entity.Entity.MarkModified();
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