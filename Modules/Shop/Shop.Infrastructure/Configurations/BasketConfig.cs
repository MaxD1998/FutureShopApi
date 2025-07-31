using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.Bases;
using Shop.Domain.Aggregates.Baskets;

namespace Shop.Infrastructure.Configurations;

public class BasketConfig : BaseConfig<BasketAggregate>
{
    protected override void ConfigureEntity(EntityTypeBuilder<BasketAggregate> builder)
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