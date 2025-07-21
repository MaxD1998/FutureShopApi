using Authorization.Domain.Aggregates.Users.Entities.RefreshTokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.Bases;

namespace Authorization.Inrfrastructure.Configurations;

public class RefreshTokenConfig : BaseConfig<RefreshToken>
{
    protected override void ConfigureEntity(EntityTypeBuilder<RefreshToken> builder)
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