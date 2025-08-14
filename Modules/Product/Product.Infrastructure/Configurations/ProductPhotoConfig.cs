using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Infrastructure.Entities;
using Shared.Infrastructure.Bases;

namespace Product.Infrastructure.Configurations;

public class ProductPhotoConfig : BaseConfig<ProductPhotoEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<ProductPhotoEntity> builder)
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