using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.Bases;
using Shared.Shared.Constants;
using Shop.Domain.Aggregates.ProductBases.Entities;

namespace Shop.Infrastructure.Configurations;

public class ProductParameterConfig : BaseConfig<ProductParameterEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<ProductParameterEntity> builder)
    {
        builder.Property(x => x.ProductBaseId)
            .HasColumnOrder(100)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(StringLengthConst.LongString)
            .HasColumnOrder(101)
            .IsRequired();

        builder.HasMany(x => x.ProductParameterValues)
            .WithOne(x => x.ProductParameter)
            .HasForeignKey(x => x.ProductParameterId);

        builder.HasMany(x => x.Translations)
            .WithOne(x => x.ProductParameter)
            .HasForeignKey(x => x.ProductParameterId);
    }
}