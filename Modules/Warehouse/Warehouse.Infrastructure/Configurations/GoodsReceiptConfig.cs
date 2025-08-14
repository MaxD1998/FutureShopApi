using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.Bases;
using Warehouse.Infrastructure.Entities;

namespace Warehouse.Infrastructure.Configurations;

public class GoodsReceiptConfig : BaseConfig<GoodsReceiptEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<GoodsReceiptEntity> builder)
    {
        builder.Property(x => x.WarehouseId)
            .HasColumnOrder(100)
            .IsRequired();
    }
}