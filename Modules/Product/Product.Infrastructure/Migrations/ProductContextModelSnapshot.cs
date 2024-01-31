﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Product.Infrastructure;

#nullable disable

namespace Product.Infrastructure.Migrations
{
    [DbContext(typeof(ProductContext))]
    partial class ProductContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Product.Domain.Entities.CategoryEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnOrder(0);

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(1);

                    b.Property<DateTime>("ModifyTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(2);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnOrder(100);

                    b.Property<Guid?>("ParentCategoryId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ParentCategoryId");

                    b.ToTable("Category", (string)null);
                });

            modelBuilder.Entity("Product.Domain.Entities.ProductBaseEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnOrder(0);

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid")
                        .HasColumnOrder(100);

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnOrder(102);

                    b.Property<DateTime>("ModifyTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(2);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnOrder(101);

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("ProductBase", (string)null);
                });

            modelBuilder.Entity("Product.Domain.Entities.ProductEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnOrder(0);

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(1);

                    b.Property<DateTime>("ModifyTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(2);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnOrder(101);

                    b.Property<Guid>("ProductBaseId")
                        .HasColumnType("uuid")
                        .HasColumnOrder(100);

                    b.HasKey("Id");

                    b.HasIndex("ProductBaseId");

                    b.ToTable("Product", (string)null);
                });

            modelBuilder.Entity("Product.Domain.Entities.ProductParameterEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnOrder(0);

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(1);

                    b.Property<DateTime>("ModifyTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(2);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnOrder(101);

                    b.Property<Guid>("ProductBaseId")
                        .HasColumnType("uuid")
                        .HasColumnOrder(100);

                    b.HasKey("Id");

                    b.HasIndex("ProductBaseId");

                    b.ToTable("ProductParameter", (string)null);
                });

            modelBuilder.Entity("Product.Domain.Entities.ProductParameterValueEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnOrder(0);

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(1);

                    b.Property<DateTime>("ModifyTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(2);

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid")
                        .HasColumnOrder(101);

                    b.Property<Guid>("ProductParameterId")
                        .HasColumnType("uuid");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnOrder(102);

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("ProductParameterId");

                    b.ToTable("ProductParameterValue", (string)null);
                });

            modelBuilder.Entity("Product.Domain.Entities.CategoryEntity", b =>
                {
                    b.HasOne("Product.Domain.Entities.CategoryEntity", "ParentCategory")
                        .WithMany("SubCategories")
                        .HasForeignKey("ParentCategoryId");

                    b.Navigation("ParentCategory");
                });

            modelBuilder.Entity("Product.Domain.Entities.ProductBaseEntity", b =>
                {
                    b.HasOne("Product.Domain.Entities.CategoryEntity", "Category")
                        .WithMany("ProductBases")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Product.Domain.Entities.ProductEntity", b =>
                {
                    b.HasOne("Product.Domain.Entities.ProductBaseEntity", "ProductBase")
                        .WithMany("Products")
                        .HasForeignKey("ProductBaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductBase");
                });

            modelBuilder.Entity("Product.Domain.Entities.ProductParameterEntity", b =>
                {
                    b.HasOne("Product.Domain.Entities.ProductBaseEntity", "ProductBase")
                        .WithMany("ProductParameters")
                        .HasForeignKey("ProductBaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductBase");
                });

            modelBuilder.Entity("Product.Domain.Entities.ProductParameterValueEntity", b =>
                {
                    b.HasOne("Product.Domain.Entities.ProductEntity", "Product")
                        .WithMany("ProductParameterValues")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Product.Domain.Entities.ProductParameterEntity", "ProductParameter")
                        .WithMany()
                        .HasForeignKey("ProductParameterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("ProductParameter");
                });

            modelBuilder.Entity("Product.Domain.Entities.CategoryEntity", b =>
                {
                    b.Navigation("ProductBases");

                    b.Navigation("SubCategories");
                });

            modelBuilder.Entity("Product.Domain.Entities.ProductBaseEntity", b =>
                {
                    b.Navigation("ProductParameters");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("Product.Domain.Entities.ProductEntity", b =>
                {
                    b.Navigation("ProductParameterValues");
                });
#pragma warning restore 612, 618
        }
    }
}
