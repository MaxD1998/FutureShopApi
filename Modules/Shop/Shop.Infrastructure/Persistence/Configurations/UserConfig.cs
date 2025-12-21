using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.Bases;
using Shared.Shared.Constants;
using Shop.Infrastructure.Persistence.Entities.Users;
using Shop.Infrastructure.Persistence.Seeds;

namespace Shop.Infrastructure.Persistence.Configurations;

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

        builder.HasMany(x => x.ProductReviews)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(x => x.UserCompanyDetails)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId);

        builder.HasMany(x => x.UserDeliveryAddresses)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId);

        builder.HasData(UserSeed.Seed());
    }
}