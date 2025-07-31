using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.Bases;
using Shop.Domain.Aggregates.Baskets.Entities;

namespace Shop.Infrastructure.Configurations;

public class BasketItemConfig : BaseConfig<BasketItemEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<BasketItemEntity> builder)
    {
        builder.Property(x => x.BasketId)
            .HasColumnOrder(100)
            .IsRequired();

        builder.Property(x => x.ProductId)
            .HasColumnOrder(101)
            .IsRequired();

        builder.Property(x => x.Quantity)
            .HasColumnOrder(102)
            .IsRequired();

        builder.HasIndex(x => new { x.BasketId, x.ProductId })
            .IsUnique();
    }
}