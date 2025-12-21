using Authorization.Domain.Entities.PrermissionGroups;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.Bases;

namespace Authorization.Infrastructure.Configurations;

public class PermissionGroupConfig : BaseConfig<PermissionGroupEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<PermissionGroupEntity> builder)
    {
        builder.Property(x => x.Name)
            .HasColumnOrder(100)
            .IsRequired();

        builder.HasIndex(x => x.Name)
            .IsUnique();

        builder.HasMany(x => x.UserPermissionGroups)
            .WithOne(x => x.PermissionGroup)
            .HasForeignKey(x => x.PermissionGroupId);

        builder.HasMany(x => x.AuthorizationPermissions)
            .WithOne(x => x.PermissionGroup)
            .HasForeignKey(x => x.PermissionGroupId);

        builder.HasMany(x => x.ProductPermissions)
            .WithOne(x => x.PermissionGroup)
            .HasForeignKey(x => x.PermissionGroupId);

        builder.HasMany(x => x.ShopPermissions)
            .WithOne(x => x.PermissionGroup)
            .HasForeignKey(x => x.PermissionGroupId);

        builder.HasMany(x => x.WarehousePermissions)
            .WithOne(x => x.PermissionGroup)
            .HasForeignKey(x => x.PermissionGroupId);
    }
}