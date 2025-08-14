using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.Bases;
using Warehouse.Infrastructure.Entities;

namespace Warehouse.Infrastructure.Configurations;

public class WarehouseConfig : BaseConfig<WarehouseEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<WarehouseEntity> builder)
    {
        builder.Property(x => x.Type)
            .HasColumnOrder(100)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasColumnOrder(101)
            .IsRequired();

        builder.Property(x => x.City)
            .HasColumnOrder(102)
            .IsRequired();

        builder.Property(x => x.PostalCode)
            .HasColumnOrder(103)
            .IsRequired();

        builder.Property(x => x.Street)
            .HasColumnOrder(104)
            .IsRequired();

        builder.Property(x => x.BuildingName)
            .HasColumnOrder(105)
            .IsRequired();

        builder.Property(x => x.FlatNumber)
            .HasColumnOrder(106);

        builder.Property(x => x.IsActive)
            .HasColumnOrder(107);

        builder.HasMany(x => x.GoodsIssues)
            .WithOne(x => x.Warehouse)
            .HasForeignKey(x => x.WarehouseId);

        builder.HasMany(x => x.GoodsReceipts)
            .WithOne(x => x.Warehouse)
            .HasForeignKey(x => x.WarehouseId);

        builder.HasMany(x => x.IncomingTransfers)
            .WithOne(x => x.DestinationWarehouse)
            .HasForeignKey(x => x.DestinationWarehouseId);

        builder.HasMany(x => x.OutgoingTransfers)
            .WithOne(x => x.SourceWarehouse)
            .HasForeignKey(x => x.SourceWarehouseId);

        builder.HasMany(x => x.ProductQuantities)
            .WithOne(x => x.Warehouse)
            .HasForeignKey(x => x.WarehouseId);
    }
}