using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Constants;
using Shop.Infrastructure.Entities;

namespace Shop.Infrastructure.Configurations;

public class ProductBaseConfig : BaseExternalConfig<ProductBaseEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<ProductBaseEntity> builder)
    {
        builder.Property(x => x.CategoryId)
            .HasColumnOrder(100)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(StringLengthConst.LongString)
            .HasColumnOrder(101)
            .IsRequired();

        builder.HasMany(x => x.ProductParameters)
            .WithOne(x => x.ProductBase)
            .HasForeignKey(x => x.ProductBaseId);

        builder.HasMany(x => x.Products)
            .WithOne(x => x.ProductBase)
            .HasForeignKey(x => x.ProductBaseId);
    }
}