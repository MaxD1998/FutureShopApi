using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.Bases;
using Shop.Infrastructure.Entities.Promotions;

namespace Shop.Infrastructure.Configurations;

public class PromotionProductConfig : BaseConfig<PromotionProductEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<PromotionProductEntity> builder)
    {
        builder.Property(x => x.PromotionId)
            .HasColumnOrder(100)
            .IsRequired();

        builder.Property(x => x.ProductId)
            .HasColumnOrder(101)
            .IsRequired();
    }
}