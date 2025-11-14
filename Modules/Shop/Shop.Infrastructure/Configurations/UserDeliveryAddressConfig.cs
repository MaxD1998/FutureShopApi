using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Constants;
using Shop.Infrastructure.Entities.Users;

namespace Shop.Infrastructure.Configurations;

public class UserDeliveryAddressConfig : BaseConfig<UserDeliveryAddressEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<UserDeliveryAddressEntity> builder)
    {
        builder.Property(x => x.UserId)
            .HasColumnOrder(100)
            .IsRequired();

        builder.Property(x => x.IsDefault)
            .HasColumnOrder(101)
            .IsRequired();

        builder.Property(x => x.Email)
            .HasColumnOrder(102)
            .HasMaxLength(StringLengthConst.LongString)
            .IsRequired();

        builder.Property(x => x.PhoneNumber)
            .HasColumnOrder(103)
            .HasMaxLength(StringLengthConst.ShortString)
            .IsRequired();

        builder.Property(x => x.PostalCode)
            .HasColumnOrder(104)
            .HasMaxLength(StringLengthConst.ShortString)
            .IsRequired();

        builder.Property(x => x.City)
            .HasColumnOrder(105)
            .HasMaxLength(StringLengthConst.LongString)
            .IsRequired();

        builder.Property(x => x.Street)
            .HasColumnOrder(106)
            .HasMaxLength(StringLengthConst.LongString)
            .IsRequired();

        builder.Property(x => x.HouseNumber)
            .HasColumnOrder(107)
            .HasMaxLength(StringLengthConst.ShortString)
            .IsRequired();

        builder.Property(x => x.ApartamentNumber)
            .HasColumnOrder(108)
            .HasMaxLength(StringLengthConst.ShortString)
            .IsRequired();
    }
}