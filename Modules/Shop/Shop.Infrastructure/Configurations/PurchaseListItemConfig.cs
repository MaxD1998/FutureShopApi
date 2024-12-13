using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.Bases;
using Shop.Domain.Entities;

namespace Shop.Infrastructure.Configurations;

public class PurchaseListItemConfig : BaseConfig<PurchaseListItemEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<PurchaseListItemEntity> builder)
    {
        builder.Property(x => x.PurchaseListId)
            .HasColumnOrder(100)
            .IsRequired();

        builder.Property(x => x.ProductId)
            .HasColumnOrder(101)
            .IsRequired();
    }
}