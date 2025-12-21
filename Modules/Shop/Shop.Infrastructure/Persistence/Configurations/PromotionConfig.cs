using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Constants;
using Shop.Infrastructure.Persistence.Entities.Promotions;

namespace Shop.Infrastructure.Persistence.Configurations;

public class PromotionConfig : BaseConfig<PromotionEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<PromotionEntity> builder)
    {
        builder.Property(x => x.AdCampaignId)
            .HasColumnOrder(100);

        builder.Property(x => x.Name)
            .HasColumnOrder(101)
            .HasMaxLength(StringLengthConst.LongString)
            .IsRequired();

        builder.Property(x => x.Code)
            .HasColumnOrder(102)
            .HasMaxLength(StringLengthConst.ShortString)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .HasColumnOrder(103)
            .IsRequired();

        builder.Property(x => x.Start)
            .HasColumnOrder(104)
            .IsRequired();

        builder.Property(x => x.End)
            .HasColumnOrder(105)
            .IsRequired();

        builder.Property(x => x.Type)
            .HasColumnOrder(106)
            .IsRequired();

        builder.Property(x => x.Value)
            .HasColumnOrder(107)
            .IsRequired();
    }
}