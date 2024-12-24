﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Constants;
using Shop.Domain.Entities;

namespace Shop.Infrastructure.Configurations;

public class ProductConfig : BaseConfig<ProductEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<ProductEntity> builder)
    {
        builder.Property(x => x.ProductBaseId)
            .HasColumnOrder(100)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(StringLengthConst.LongString)
            .HasColumnOrder(101)
            .IsRequired();

        builder.Property(x => x.Price)
            .HasColumnOrder(102)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasColumnOrder(103);

        builder.HasMany(x => x.BasketItems)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductId);

        builder.HasMany(x => x.ProductParameterValues)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductId);

        builder.HasMany(x => x.ProductPhotos)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductId);

        builder.HasMany(x => x.PurchaseListItems)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductId);

        builder.HasMany(x => x.Translations)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductId);
    }
}