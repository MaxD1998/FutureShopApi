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

        builder.Property(x => x.FirstName)
            .HasColumnOrder(102)
            .HasMaxLength(StringLengthConst.MiddleString)
            .IsRequired();

        builder.Property(x => x.LastName)
            .HasColumnOrder(103)
            .HasMaxLength(StringLengthConst.LongString)
            .IsRequired();

        builder.Property(x => x.Email)
            .HasColumnOrder(104)
            .HasMaxLength(StringLengthConst.LongString)
            .IsRequired();

        builder.Property(x => x.PhoneNumber)
            .HasColumnOrder(105)
            .HasMaxLength(StringLengthConst.ShortString)
            .IsRequired();

        builder.Property(x => x.PostalCode)
            .HasColumnOrder(106)
            .HasMaxLength(StringLengthConst.ShortString)
            .IsRequired();

        builder.Property(x => x.City)
            .HasColumnOrder(107)
            .HasMaxLength(StringLengthConst.LongString)
            .IsRequired();

        builder.Property(x => x.Street)
            .HasColumnOrder(108)
            .HasMaxLength(StringLengthConst.LongString)
            .IsRequired();

        builder.Property(x => x.HouseNumber)
            .HasColumnOrder(109)
            .HasMaxLength(StringLengthConst.ShortString)
            .IsRequired();

        builder.Property(x => x.ApartamentNumber)
            .HasColumnOrder(110)
            .HasMaxLength(StringLengthConst.ShortString)
            .IsRequired();
    }
}