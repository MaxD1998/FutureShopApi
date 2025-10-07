using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.Bases;
using Shop.Infrastructure.Entities.ProductBases;

namespace Shop.Infrastructure.Configurations;

public class ProductParameterTranslationConfig : BaseTranslationConfig<ProductParameterTranslationEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<ProductParameterTranslationEntity> builder)
    {
        base.ConfigureEntity(builder);

        builder.Property(x => x.ProductParameterId)
            .HasColumnOrder(100)
            .IsRequired();

        builder.HasIndex(x => new { x.ProductParameterId, x.Lang })
            .IsUnique();
    }
}