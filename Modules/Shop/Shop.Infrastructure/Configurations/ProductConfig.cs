using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Constants;
using Shop.Infrastructure.Entities.Products;

namespace Shop.Infrastructure.Configurations;

public class ProductConfig : BaseExternalConfig<ProductEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<ProductEntity> builder)
    {
        builder.Property(x => x.ProductBaseId)
            .HasColumnOrder(100)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(StringLengthConst.LongString)
            .HasColumnOrder(101)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .HasColumnOrder(102)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasMany(x => x.AdCampaignProducts)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductId);

        builder.HasMany(x => x.BasketItems)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductId);

        builder.HasMany(x => x.Prices)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductId);

        builder.HasMany(x => x.ProductParameterValues)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductId);

        builder.HasMany(x => x.ProductPhotos)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductId);

        builder.HasMany(x => x.ProductReviews)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductId);

        builder.HasMany(x => x.PromotionProducts)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductId);

        builder.HasMany(x => x.PurchaseListItems)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductId);

        builder.HasMany(x => x.Translations)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductId);
    }
}