﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Constants;
using Shop.Domain.Entities;

namespace Shop.Infrastructure.Configurations;

public class ProductParameterValueConfig : BaseConfig<ProductParameterValueEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<ProductParameterValueEntity> builder)
    {
        builder.Property(x => x.ProductId)
            .HasColumnOrder(100)
            .IsRequired();

        builder.Property(x => x.ProductParameterId)
            .HasColumnOrder(101)
            .IsRequired();

        builder.Property(x => x.Value)
            .HasMaxLength(StringLengthConst.MiddleString)
            .HasColumnOrder(102)
            .IsRequired();
    }
}