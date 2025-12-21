using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.Bases;
using Shop.Domain.Entities.Products;

namespace Shop.Infrastructure.Persistence.Configurations;

public class PriceConfig : BaseConfig<PriceEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<PriceEntity> builder)
    {
        builder.Property(x => x.ProductId)
            .HasColumnOrder(100)
            .IsRequired();

        builder.Property(x => x.Price)
            .HasColumnOrder(101)
            .IsRequired();

        builder.Property(x => x.Start)
            .HasColumnOrder(102);

        builder.Property(x => x.End)
            .HasColumnOrder(103);
    }
}