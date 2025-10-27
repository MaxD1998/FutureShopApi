﻿using Authorization.Infrastructure.Entities.Permissions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.Bases;

namespace Authorization.Infrastructure.Configurations;

public class ProductPermissionConfig : BaseConfig<ProductPermissionEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<ProductPermissionEntity> builder)
    {
        builder.Property(x => x.UserPermissionModuleId)
            .HasColumnOrder(100)
            .IsRequired();

        builder.Property(x => x.Permission)
            .HasColumnOrder(101)
            .IsRequired();

        builder.HasIndex(x => new { x.UserPermissionModuleId, x.Permission })
            .IsUnique();
    }
}