using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Aggregates.Products;
using Shared.Infrastructure.Bases;
using Shared.Shared.Constants;

namespace Product.Infrastructure.Configurations;

public class ProductConfig : BaseConfig<ProductAggregate>
{
    protected override void ConfigureEntity(EntityTypeBuilder<ProductAggregate> builder)
    {
        builder.Property(x => x.ProductBaseId)
            .HasColumnOrder(100)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(StringLengthConst.LongString)
            .HasColumnOrder(101)
            .IsRequired();

        builder.HasMany(x => x.ProductPhotos)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductId);
    }
}