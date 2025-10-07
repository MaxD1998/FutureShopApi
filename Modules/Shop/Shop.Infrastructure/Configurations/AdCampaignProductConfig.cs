using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.Bases;
using Shop.Infrastructure.Entities.AdCampaigns;

namespace Shop.Infrastructure.Configurations;

public class AdCampaignProductConfig : BaseConfig<AdCampaignProductEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<AdCampaignProductEntity> builder)
    {
        builder.Property(x => x.AdCampaignId)
            .HasColumnOrder(100)
            .IsRequired();

        builder.Property(x => x.ProductId)
            .HasColumnOrder(101)
            .IsRequired();
    }
}