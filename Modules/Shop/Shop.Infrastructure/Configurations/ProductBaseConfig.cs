using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.Bases;
using Shop.Domain.Entities;

namespace Shop.Infrastructure.Configurations;

public class ProductBaseConfig : BaseConfig<ProductBaseEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<ProductBaseEntity> builder)
    {
        builder.Property(x => x.CategoryId)
            .HasColumnOrder(100)
            .IsRequired();

        builder.HasMany(x => x.ProductParameters)
            .WithOne(x => x.ProductBase)
            .HasForeignKey(x => x.ProductBaseId);

        builder.HasMany(x => x.Products)
            .WithOne(x => x.ProductBase)
            .HasForeignKey(x => x.ProductBaseId);
    }
}