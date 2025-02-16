﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Constants;
using Shop.Domain.Entities;

namespace Shop.Infrastructure.Configurations;

public class CategoryConfig : BaseExternalConfig<CategoryEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<CategoryEntity> builder)
    {
        builder.Property(x => x.Name)
            .HasMaxLength(StringLengthConst.LongString)
            .HasColumnOrder(100)
            .IsRequired();

        builder.HasMany(x => x.SubCategories)
            .WithOne(x => x.ParentCategory)
            .HasForeignKey(x => x.ParentCategoryId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(x => x.ProductBases)
            .WithOne(x => x.Category)
            .HasForeignKey(x => x.CategoryId);

        builder.HasMany(x => x.Translations)
            .WithOne(x => x.Category)
            .HasForeignKey(x => x.CategoryId);
    }
}