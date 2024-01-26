using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Bases;

namespace Infrastructure.Configurations;

public class UserConfig : BaseConfig<UserEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<UserEntity> builder)
    {
        builder.Property(x => x.FirsName)
            .HasColumnOrder(100)
            .IsRequired();

        builder.Property(x => x.LastName)
            .HasColumnOrder(101)
            .IsRequired();

        builder.Property(x => x.Email)
            .HasColumnOrder(102)
            .IsRequired();

        builder.Property(x => x.HashedPassword)
            .HasColumnOrder(103)
            .IsRequired();

        builder.Property(x => x.DateOfBirth)
            .HasColumnOrder(104)
            .IsRequired();

        builder.Property(x => x.Type)
            .HasColumnOrder(105)
            .IsRequired();

        builder.HasOne(x => x.RefreshToken)
            .WithOne(x => x.User)
            .HasForeignKey<RefreshTokenEntity>(x => x.UserId);
    }
}