using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Entities;
using Shared.Infrastructure.Bases;

namespace Product.Infrastructure.Configurations;

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