using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.Bases;
using Shop.Domain.Entities.Promotions;

namespace Shop.Infrastructure.Persistence.Configurations;

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