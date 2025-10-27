using Authorization.Infrastructure.Entities.PrermissionGroups;
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

        builder.HasMany(x => x.UserPermissionModules)
            .WithOne(x => x.UserPermissionGroup)
            .HasForeignKey(x => x.UserPermissionGroupId);

        builder.HasMany(x => x.Users)
            .WithOne(x => x.UserPermissionGroup)
            .HasForeignKey(x => x.UserPermissionGroupId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}