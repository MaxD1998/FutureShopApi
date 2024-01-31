using Authorization.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.Bases;

namespace Authorization.Inrfrastructure.Configurations;

public class RefreshTokenConfig : BaseConfig<RefreshTokenEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<RefreshTokenEntity> builder)
    {
        builder.Property(x => x.UserId)
            .HasColumnOrder(100)
            .IsRequired();

        builder.Property(x => x.Token)
            .HasColumnOrder(101)
            .IsRequired();

        builder.Property(x => x.StartDate)
            .HasColumnOrder(102)
            .IsRequired();

        builder.Property(x => x.EndDate)
            .HasColumnOrder(103)
            .IsRequired();
    }
}