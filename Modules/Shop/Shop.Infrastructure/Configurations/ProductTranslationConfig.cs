﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.Bases;
using Shop.Infrastructure.Entities;

namespace Shop.Infrastructure.Configurations;

public class ProductTranslationConfig : BaseTranslationConfig<ProductTranslationEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<ProductTranslationEntity> builder)
    {
        base.ConfigureEntity(builder);

        builder.Property(x => x.ProductId)
            .HasColumnOrder(100)
            .IsRequired();

        builder.HasIndex(x => new { x.ProductId, x.Lang })
            .IsUnique();
    }
}