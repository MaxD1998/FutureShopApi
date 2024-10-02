using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Entities;
using Shared.Infrastructure.Bases;

namespace Product.Infrastructure.Configurations;

public class UserConfig : BaseConfig<UserEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<UserEntity> builder)
    {
        builder.Property(x => x.FirstName)
            .HasColumnOrder(100)
            .IsRequired();

        builder.Property(x => x.LastName)
            .HasColumnOrder(101)
            .IsRequired();

        builder.HasMany(x => x.PurchaseLists)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId);
    }
}