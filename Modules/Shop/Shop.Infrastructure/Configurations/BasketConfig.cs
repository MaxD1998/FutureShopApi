using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.Bases;
using Shop.Infrastructure.Entities.Baskets;

namespace Shop.Infrastructure.Configurations;

public class BasketConfig : BaseConfig<BasketEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<BasketEntity> builder)
    {
        builder.Property(x => x.UserId)
            .HasColumnOrder(100);

        builder.HasMany(x => x.BasketItems)
            .WithOne(x => x.Basket)
            .HasForeignKey(x => x.BasketId);

        builder.HasIndex(x => x.UserId)
            .IsUnique();
    }
}