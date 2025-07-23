using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Aggregates.Products.Entities;
using Shared.Infrastructure.Bases;

namespace Product.Infrastructure.Configurations;

public class ProductPhotoConfig : BaseConfig<ProductPhoto>
{
    protected override void ConfigureEntity(EntityTypeBuilder<ProductPhoto> builder)
    {
        builder.Property(x => x.ProductId)
            .HasColumnOrder(100)
            .IsRequired();

        builder.Property(x => x.FileId)
            .HasColumnOrder(101)
            .IsRequired();

        builder.Property(x => x.Position)
            .HasColumnOrder(102)
            .IsRequired();

        builder.HasIndex(x => new { x.FileId })
            .IsUnique();

        builder.HasIndex(x => new { x.ProductId, x.Position })
            .IsUnique();
    }
}