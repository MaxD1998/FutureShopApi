using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Constants;
using Shop.Domain.Entities;

namespace Shop.Infrastructure.Configurations;

public class AdCampaignConfig : BaseConfig<AdCampaignEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<AdCampaignEntity> builder)
    {
        builder.Property(x => x.Name)
            .HasMaxLength(StringLengthConst.LongString)
            .HasColumnOrder(100)
            .IsRequired();

        builder.Property(x => x.Start)
            .HasColumnOrder(101)
            .IsRequired();

        builder.Property(x => x.End)
            .HasColumnOrder(102)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .HasColumnOrder(103)
            .IsRequired();

        builder.HasMany(x => x.AdCampaignItems)
            .WithOne(x => x.AdCampaign)
            .HasForeignKey(x => x.AdCampaignId);
    }
}