using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.Bases;
using Shop.Infrastructure.Persistence.Entities.AdCampaigns;

namespace Shop.Infrastructure.Persistence.Configurations;

public class AdCampaignItemConfig : BaseConfig<AdCampaignItemEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<AdCampaignItemEntity> builder)
    {
        builder.Property(x => x.AdCampaignId)
                .HasColumnOrder(100)
                .IsRequired();

        builder.Property(x => x.FileId)
            .HasColumnOrder(101)
            .IsRequired();

        builder.Property(x => x.Position)
            .HasColumnOrder(102)
            .IsRequired();

        builder.Property(x => x.Lang)
            .HasColumnOrder(103)
            .IsRequired();

        builder.HasIndex(x => new { x.FileId })
            .IsUnique();

        builder.HasIndex(x => new { x.AdCampaignId, x.Position, x.Lang })
            .IsUnique();
    }
}