using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.Bases;
using Shop.Infrastructure.Entities.Products;

namespace Shop.Infrastructure.Configurations;

public class ProductReviewConfig : BaseConfig<ProductReviewEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<ProductReviewEntity> builder)
    {
        builder.Property(x => x.ProductId)
            .HasColumnOrder(100)
            .IsRequired();

        builder.Property(x => x.UserId)
            .HasColumnOrder(101)
            .IsRequired();

        builder.Property(x => x.Rating)
            .HasColumnOrder(102)
            .IsRequired();

        builder.Property(x => x.Comment)
            .HasColumnOrder(103);

        builder.HasIndex(x => new { x.ProductId, x.UserId })
            .IsUnique();

        var tableName = typeof(ProductReviewEntity).Name.Replace("Entity", string.Empty);
        builder.ToTable(x => x.HasCheckConstraint($"CK_{tableName}_{nameof(ProductReviewEntity.Rating)}_Range", $"\"{nameof(ProductReviewEntity.Rating)}\" BETWEEN 1 AND 5"));
    }
}