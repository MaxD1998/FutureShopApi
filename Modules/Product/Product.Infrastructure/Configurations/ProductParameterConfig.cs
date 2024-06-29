using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Entities;
using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Constants;

namespace Product.Infrastructure.Configurations;

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

        builder.HasMany(x => x.Translations)
            .WithOne(x => x.ProductParameter)
            .HasForeignKey(x => x.ProductParameterId);
    }
}