using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.Bases;
using Shop.Domain.Aggregates.Categories.Entities;

namespace Shop.Infrastructure.Configurations;

public class CategoryTranslationConfig : BaseTranslationConfig<CategoryTranslationEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<CategoryTranslationEntity> builder)
    {
        base.ConfigureEntity(builder);

        builder.Property(x => x.CategoryId)
            .HasColumnOrder(100)
            .IsRequired();

        builder.HasIndex(x => new { x.CategoryId, x.Lang })
            .IsUnique();
    }
}