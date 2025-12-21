using Authorization.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.Bases;

namespace Authorization.Infrastructure.Configurations;

public class UserPermissionGroupConfig : BaseConfig<UserPermissionGroupEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<UserPermissionGroupEntity> builder)
    {
        builder.Property(x => x.UserId)
            .HasColumnOrder(100)
            .IsRequired();

        builder.Property(x => x.PermissionGroupId)
            .HasColumnOrder(101)
            .IsRequired();

        builder.HasIndex(x => new { x.UserId, x.PermissionGroupId })
            .IsUnique();
    }
}