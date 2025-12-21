using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.Bases;
using Warehouse.Domain.Entities;

namespace Warehouse.Infrastructure.Configurations;

public class InterWarehouseTransferProductsConfig : BaseConfig<InterWarehouseTransferProductsEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<InterWarehouseTransferProductsEntity> builder)
    {
        builder.Property(x => x.InterWarehouseTrasferId)
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