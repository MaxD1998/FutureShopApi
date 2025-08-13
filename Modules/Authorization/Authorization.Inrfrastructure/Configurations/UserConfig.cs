using Authorization.Infrastructure.Entities;
using Authorization.Infrastructure.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Constants;

namespace Authorization.Infrastructure.Configurations;

public class UserConfig : BaseConfig<UserEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<UserEntity> builder)
    {
        builder.Property(x => x.FirstName)
            .HasColumnOrder(100)
            .HasMaxLength(StringLengthConst.MiddleString)
            .IsRequired();

        builder.Property(x => x.LastName)
            .HasColumnOrder(101)
            .HasMaxLength(StringLengthConst.LongString)
            .IsRequired();

        builder.Property(x => x.PhoneNumber)
            .HasColumnOrder(103)
            .HasMaxLength(StringLengthConst.ShortString);

        builder.Property(x => x.Email)
            .HasColumnOrder(104)
            .HasMaxLength(StringLengthConst.LongString)
            .IsRequired();

        builder.Property(x => x.HashedPassword)
            .HasColumnOrder(105)
            .IsRequired();

        builder.Property(x => x.DateOfBirth)
            .HasColumnOrder(106)
            .IsRequired();

        builder.Property(x => x.Type)
            .HasColumnOrder(107)
            .IsRequired();

        builder.HasIndex(x => x.Email)
            .IsUnique();

        builder.HasOne(x => x.RefreshToken)
            .WithOne(x => x.User)
            .HasForeignKey<RefreshTokenEntity>(x => x.UserId);

        builder.HasMany(x => x.UserModules)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId);

        builder.HasData(UserSeed.Seed());
    }
}