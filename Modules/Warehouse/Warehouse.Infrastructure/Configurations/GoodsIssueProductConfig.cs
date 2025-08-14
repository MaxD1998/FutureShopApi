using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.Bases;
using Warehouse.Infrastructure.Entities;

namespace Warehouse.Infrastructure.Configurations;

public class GoodsIssueProductConfig : BaseConfig<GoodsIssueProductsEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<GoodsIssueProductsEntity> builder)
    {
        builder.Property(x => x.GoodsIssueId)
            .HasColumnOrder(100)
            .IsRequired();

        builder.Property(x => x.ProductId)
            .HasColumnOrder(101)
            .IsRequired();

        builder.Property(x => x.Quantity)
            .HasColumnOrder(102)
            .IsRequired();
    }
}