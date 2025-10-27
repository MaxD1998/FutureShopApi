using Authorization.Infrastructure.Entities.Permissions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.Bases;

namespace Authorization.Infrastructure.Configurations;

public class PermissionModuleConfig : BaseConfig<PermissionModuleEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<PermissionModuleEntity> builder)
    {
        builder.Property(x => x.UserId)
            .HasColumnOrder(100)
            .IsRequired();

        builder.Property(x => x.Type)
            .HasColumnOrder(101)
            .IsRequired();

        builder.HasIndex(x => new { x.UserId, x.Type })
            .IsUnique();
    }
}