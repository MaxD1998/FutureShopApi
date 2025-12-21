using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.Bases;
using Warehouse.Domain.Entities;

namespace Warehouse.Infrastructure.Configurations;

public class ProductConfig : BaseConfig<ProductEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<ProductEntity> builder)
    {
        builder.Property(x => x.Name)
            .HasColumnOrder(100)
            .IsRequired();

        builder.HasMany(x => x.GoodsIssueProducts)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductId);

        builder.HasMany(x => x.GoodsReceiptProducts)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductId);

        builder.HasMany(x => x.InterWarehouseTransferProducts)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductId);

        builder.HasMany(x => x.ProductQuantities)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductId);
    }
}