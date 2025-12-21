using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.Bases;
using Warehouse.Domain.Entities;

namespace Warehouse.Infrastructure.Configurations;

public class ProductQuantityConfig : BaseConfig<ProductQuantityEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<ProductQuantityEntity> builder)
    {
        builder.Property(x => x.ProductId)
            .HasColumnOrder(100)
            .IsRequired();

        builder.Property(x => x.WarehouseId)
            .HasColumnOrder(101)
            .IsRequired();
    }
}