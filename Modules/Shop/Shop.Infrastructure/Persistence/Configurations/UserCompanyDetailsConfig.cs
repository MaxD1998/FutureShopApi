using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Constants;
using Shop.Infrastructure.Persistence.Entities.Users;

namespace Shop.Infrastructure.Persistence.Configurations;

public class UserCompanyDetailsConfig : BaseConfig<UserCompanyDetailsEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<UserCompanyDetailsEntity> builder)
    {
        builder.Property(x => x.UserId)
            .HasColumnOrder(100)
            .IsRequired();

        builder.Property(x => x.IsDefault)
            .HasColumnOrder(101)
            .IsRequired();

        builder.Property(x => x.Type)
            .HasColumnOrder(102)
            .IsRequired();

        builder.Property(x => x.CompanyName)
            .HasColumnOrder(103)
            .HasMaxLength(StringLengthConst.LongString)
            .IsRequired();

        builder.Property(x => x.CompanyIdentifierNumber)
            .HasColumnOrder(104)
            .HasMaxLength(StringLengthConst.ShortString)
            .IsRequired();

        builder.Property(x => x.PostalCode)
            .HasColumnOrder(105)
            .HasMaxLength(StringLengthConst.ShortString)
            .IsRequired();

        builder.Property(x => x.City)
            .HasColumnOrder(106)
            .HasMaxLength(StringLengthConst.LongString)
            .IsRequired();

        builder.Property(x => x.Street)
            .HasColumnOrder(107)
            .HasMaxLength(StringLengthConst.LongString)
            .IsRequired();

        builder.Property(x => x.HouseNumber)
            .HasColumnOrder(108)
            .HasMaxLength(StringLengthConst.ShortString)
            .IsRequired();

        builder.Property(x => x.ApartamentNumber)
            .HasColumnOrder(109)
            .HasMaxLength(StringLengthConst.ShortString);
    }
}