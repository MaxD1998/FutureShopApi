using Authorization.Domain.Entities.Permissions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.Bases;

namespace Authorization.Infrastructure.Configurations;

public class ShopPermissionConfig : BaseConfig<ShopPermissionEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<ShopPermissionEntity> builder)
    {
        builder.Property(x => x.PermissionGroupId)
            .HasColumnOrder(100)
            .IsRequired();

        builder.Property(x => x.Permission)
            .HasColumnOrder(101)
            .IsRequired();

        builder.HasIndex(x => new { x.PermissionGroupId, x.Permission })
            .IsUnique();
    }
}