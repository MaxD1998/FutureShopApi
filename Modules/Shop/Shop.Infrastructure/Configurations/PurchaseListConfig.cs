﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.Bases;
using Shop.Domain.Entities;

namespace Shop.Infrastructure.Configurations;

public class PurchaseListConfig : BaseConfig<PurchaseListEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<PurchaseListEntity> builder)
    {
        builder.Property(x => x.UserId)
            .HasColumnOrder(100);

        builder.Property(x => x.Name)
            .HasColumnOrder(101);

        builder.Property(x => x.IsFavourite)
            .HasColumnOrder(102)
            .IsRequired();

        builder.HasMany(x => x.PurchaseListItems)
            .WithOne(x => x.PurchaseList)
            .HasForeignKey(x => x.PurchaseListId);
    }
}