using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.Bases;
using Warehouse.Infrastructure.Entities;

namespace Warehouse.Infrastructure.Configurations;

public class GoodsIssueConfig : BaseConfig<GoodsIssueEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<GoodsIssueEntity> builder)
    {
        builder.Property(x => x.WarehouseId)
            .HasColumnOrder(100)
            .IsRequired();

        builder.Property(x => x.Type)
            .HasColumnOrder(101)
            .IsRequired();

        builder.HasMany(x => x.GoodsIssueProducts)
            .WithOne(x => x.GoodsIssue)
            .HasForeignKey(x => x.GoodsIssueId);
    }
}