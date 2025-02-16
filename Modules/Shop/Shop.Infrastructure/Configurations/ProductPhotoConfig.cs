using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.Bases;
using Shop.Domain.Entities;

namespace Shop.Infrastructure.Configurations;

public class ProductPhotoConfig : BaseExternalConfig<ProductPhotoEntity>
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