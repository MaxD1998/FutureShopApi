using Domain.Entities;
using Infrastructure.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Bases;
using Shared.Constants;

namespace Infrastructure.Configurations;

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

        builder.HasOne(x => x.RefreshToken)
            .WithOne(x => x.User)
            .HasForeignKey<RefreshTokenEntity>(x => x.UserId);

        builder.HasData(UserSeed.Seed());
    }
}