using Authorization.Domain.Aggregates.Users.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.Bases;

namespace Authorization.Inrfrastructure.Configurations;

public class UserModuleConfig : BaseConfig<UserModuleEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<UserModuleEntity> builder)
    {
        builder.Property(x => x.UserId)
            .HasColumnOrder(100)
            .IsRequired();

        builder.Property(x => x.Type)
            .HasColumnOrder(101)
            .IsRequired();

        builder.Property(x => x.CanEdit)
            .HasColumnOrder(102)
            .IsRequired();

        builder.Property(x => x.CanDelete)
            .HasColumnOrder(103)
            .IsRequired();

        builder.HasIndex(x => new { x.UserId, x.Type })
            .IsUnique();
    }
}