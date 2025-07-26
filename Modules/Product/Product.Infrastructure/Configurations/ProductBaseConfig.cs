using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Aggregates.ProductBases;
using Shared.Infrastructure.Bases;
using Shared.Shared.Constants;

namespace Product.Infrastructure.Configurations;

public class ProductBaseConfig : BaseConfig<ProductBaseAggregate>
{
    protected override void ConfigureEntity(EntityTypeBuilder<ProductBaseAggregate> builder)
    {
        builder.Property(x => x.CategoryId)
            .HasColumnOrder(100)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(StringLengthConst.LongString)
            .HasColumnOrder(101)
            .IsRequired();

        builder.HasMany(x => x.Products)
            .WithOne()
            .HasForeignKey(x => x.ProductBaseId);
    }
}