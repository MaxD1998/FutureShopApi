﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Warehouse.Infrastructure;

#nullable disable

namespace Warehouse.Infrastructure.Migrations
{
    [DbContext(typeof(WarehouseContext))]
    [Migration("20241224094740_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Warehouse.Domain.Entities.GoodsIssueEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnOrder(0);

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(1);

                    b.Property<DateTime?>("ModifyTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(2);

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnOrder(101);

                    b.Property<Guid>("WarehouseId")
                        .HasColumnType("uuid")
                        .HasColumnOrder(100);

                    b.HasKey("Id");

                    b.HasIndex("WarehouseId");

                    b.ToTable("GoodsIssue", (string)null);
                });

            modelBuilder.Entity("Warehouse.Domain.Entities.GoodsIssueProductsEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnOrder(0);

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(1);

                    b.Property<Guid>("GoodsIssueId")
                        .HasColumnType("uuid")
                        .HasColumnOrder(100);

                    b.Property<DateTime?>("ModifyTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(2);

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid")
                        .HasColumnOrder(101);

                    b.Property<int>("Quantity")
                        .HasColumnType("integer")
                        .HasColumnOrder(102);

                    b.HasKey("Id");

                    b.HasIndex("GoodsIssueId");

                    b.HasIndex("ProductId");

                    b.ToTable("GoodsIssueProducts", (string)null);
                });

            modelBuilder.Entity("Warehouse.Domain.Entities.GoodsReceiptEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnOrder(0);

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(1);

                    b.Property<DateTime?>("ModifyTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(2);

                    b.Property<Guid>("WarehouseId")
                        .HasColumnType("uuid")
                        .HasColumnOrder(100);

                    b.HasKey("Id");

                    b.HasIndex("WarehouseId");

                    b.ToTable("GoodsReceipt", (string)null);
                });

            modelBuilder.Entity("Warehouse.Domain.Entities.GoodsReceiptProductEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnOrder(0);

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(1);

                    b.Property<Guid>("GoodsReceiptId")
                        .HasColumnType("uuid")
                        .HasColumnOrder(100);

                    b.Property<DateTime?>("ModifyTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(2);

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid")
                        .HasColumnOrder(101);

                    b.Property<int>("Quantity")
                        .HasColumnType("integer")
                        .HasColumnOrder(102);

                    b.HasKey("Id");

                    b.HasIndex("GoodsReceiptId");

                    b.HasIndex("ProductId");

                    b.ToTable("GoodsReceiptProduct", (string)null);
                });

            modelBuilder.Entity("Warehouse.Domain.Entities.InterWarehouseTransferEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnOrder(0);

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(1);

                    b.Property<DateTime>("DateOfReceipt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(102);

                    b.Property<Guid>("DestinationWarehouseId")
                        .HasColumnType("uuid")
                        .HasColumnOrder(101);

                    b.Property<DateTime?>("ModifyTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(2);

                    b.Property<Guid>("SourceWarehouseId")
                        .HasColumnType("uuid")
                        .HasColumnOrder(100);

                    b.HasKey("Id");

                    b.HasIndex("DestinationWarehouseId");

                    b.HasIndex("SourceWarehouseId");

                    b.ToTable("InterWarehouseTransfer", (string)null);
                });

            modelBuilder.Entity("Warehouse.Domain.Entities.InterWarehouseTransferProductsEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnOrder(0);

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(1);

                    b.Property<Guid>("InterWarehouseTrasferId")
                        .HasColumnType("uuid")
                        .HasColumnOrder(100);

                    b.Property<DateTime?>("ModifyTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(2);

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid")
                        .HasColumnOrder(101);

                    b.Property<int>("Quantity")
                        .HasColumnType("integer")
                        .HasColumnOrder(102);

                    b.HasKey("Id");

                    b.HasIndex("InterWarehouseTrasferId");

                    b.HasIndex("ProductId");

                    b.ToTable("InterWarehouseTransferProducts", (string)null);
                });

            modelBuilder.Entity("Warehouse.Domain.Entities.ProductEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnOrder(0);

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(1);

                    b.Property<DateTime?>("ModifyTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(2);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnOrder(100);

                    b.HasKey("Id");

                    b.ToTable("Product", (string)null);
                });

            modelBuilder.Entity("Warehouse.Domain.Entities.ProductQuantityEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnOrder(0);

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(1);

                    b.Property<DateTime?>("ModifyTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(2);

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid")
                        .HasColumnOrder(100);

                    b.Property<Guid>("WarehouseId")
                        .HasColumnType("uuid")
                        .HasColumnOrder(101);

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("WarehouseId");

                    b.ToTable("ProductQuantity", (string)null);
                });

            modelBuilder.Entity("Warehouse.Domain.Entities.WarehouseEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnOrder(0);

                    b.Property<string>("BuildingName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnOrder(105);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnOrder(102);

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(1);

                    b.Property<string>("FlatNumber")
                        .HasColumnType("text")
                        .HasColumnOrder(106);

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnOrder(107);

                    b.Property<DateTime?>("ModifyTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(2);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnOrder(101);

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnOrder(103);

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnOrder(104);

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnOrder(100);

                    b.HasKey("Id");

                    b.ToTable("Warehouse", (string)null);
                });

            modelBuilder.Entity("Warehouse.Domain.Entities.GoodsIssueEntity", b =>
                {
                    b.HasOne("Warehouse.Domain.Entities.WarehouseEntity", "Warehouse")
                        .WithMany("GoodsIssues")
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("Warehouse.Domain.Entities.GoodsIssueProductsEntity", b =>
                {
                    b.HasOne("Warehouse.Domain.Entities.GoodsIssueEntity", "GoodsIssue")
                        .WithMany("GoodsIssueProducts")
                        .HasForeignKey("GoodsIssueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Warehouse.Domain.Entities.ProductEntity", "Product")
                        .WithMany("GoodsIssueProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GoodsIssue");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Warehouse.Domain.Entities.GoodsReceiptEntity", b =>
                {
                    b.HasOne("Warehouse.Domain.Entities.WarehouseEntity", "Warehouse")
                        .WithMany("GoodsReceipts")
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("Warehouse.Domain.Entities.GoodsReceiptProductEntity", b =>
                {
                    b.HasOne("Warehouse.Domain.Entities.GoodsReceiptEntity", "GoodsReceipt")
                        .WithMany("GoodsReceiptProducts")
                        .HasForeignKey("GoodsReceiptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Warehouse.Domain.Entities.ProductEntity", "Product")
                        .WithMany("GoodsReceiptProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GoodsReceipt");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Warehouse.Domain.Entities.InterWarehouseTransferEntity", b =>
                {
                    b.HasOne("Warehouse.Domain.Entities.WarehouseEntity", "DestinationWarehouse")
                        .WithMany("IncomingTransfers")
                        .HasForeignKey("DestinationWarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Warehouse.Domain.Entities.WarehouseEntity", "SourceWarehouse")
                        .WithMany("OutgoingTransfers")
                        .HasForeignKey("SourceWarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DestinationWarehouse");

                    b.Navigation("SourceWarehouse");
                });

            modelBuilder.Entity("Warehouse.Domain.Entities.InterWarehouseTransferProductsEntity", b =>
                {
                    b.HasOne("Warehouse.Domain.Entities.InterWarehouseTransferEntity", "InterWarehouseTransfer")
                        .WithMany("InterWarehouseTransferProducts")
                        .HasForeignKey("InterWarehouseTrasferId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Warehouse.Domain.Entities.ProductEntity", "Product")
                        .WithMany("InterWarehouseTransferProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InterWarehouseTransfer");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Warehouse.Domain.Entities.ProductQuantityEntity", b =>
                {
                    b.HasOne("Warehouse.Domain.Entities.ProductEntity", "Product")
                        .WithMany("ProductQuantities")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Warehouse.Domain.Entities.WarehouseEntity", "Warehouse")
                        .WithMany("ProductQuantities")
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("Warehouse.Domain.Entities.GoodsIssueEntity", b =>
                {
                    b.Navigation("GoodsIssueProducts");
                });

            modelBuilder.Entity("Warehouse.Domain.Entities.GoodsReceiptEntity", b =>
                {
                    b.Navigation("GoodsReceiptProducts");
                });

            modelBuilder.Entity("Warehouse.Domain.Entities.InterWarehouseTransferEntity", b =>
                {
                    b.Navigation("InterWarehouseTransferProducts");
                });

            modelBuilder.Entity("Warehouse.Domain.Entities.ProductEntity", b =>
                {
                    b.Navigation("GoodsIssueProducts");

                    b.Navigation("GoodsReceiptProducts");

                    b.Navigation("InterWarehouseTransferProducts");

                    b.Navigation("ProductQuantities");
                });

            modelBuilder.Entity("Warehouse.Domain.Entities.WarehouseEntity", b =>
                {
                    b.Navigation("GoodsIssues");

                    b.Navigation("GoodsReceipts");

                    b.Navigation("IncomingTransfers");

                    b.Navigation("OutgoingTransfers");

                    b.Navigation("ProductQuantities");
                });
#pragma warning restore 612, 618
        }
    }
}
