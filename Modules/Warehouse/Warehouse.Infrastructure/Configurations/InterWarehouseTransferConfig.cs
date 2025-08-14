using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.Bases;
using Warehouse.Infrastructure.Entities;

namespace Warehouse.Infrastructure.Configurations;

public class InterWarehouseTransferConfig : BaseConfig<InterWarehouseTransferEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<InterWarehouseTransferEntity> builder)
    {
        builder.Property(x => x.SourceWarehouseId)
            .HasColumnOrder(100)
            .IsRequired();

        builder.Property(x => x.DestinationWarehouseId)
            .HasColumnOrder(101)
            .IsRequired();

        builder.Property(x => x.DateOfReceipt)
            .HasColumnOrder(102)
            .IsRequired();

        builder.HasMany(x => x.InterWarehouseTransferProducts)
            .WithOne(x => x.InterWarehouseTransfer)
            .HasForeignKey(x => x.InterWarehouseTrasferId);
    }
}