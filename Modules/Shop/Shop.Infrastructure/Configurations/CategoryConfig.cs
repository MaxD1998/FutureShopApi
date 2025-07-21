using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.Bases;
using Shared.Shared.Constants;
using Shop.Domain.Entities;

namespace Shop.Infrastructure.Configurations;

public class CategoryConfig : BaseExternalConfig<CategoryEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<CategoryEntity> builder)
    {
        builder.Property(x => x.ParentCategoryId)
            .HasColumnOrder(100);

        builder.Property(x => x.Name)
            .HasMaxLength(StringLengthConst.LongString)
            .HasColumnOrder(101)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .HasColumnOrder(102)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasMany(x => x.SubCategories)
            .WithOne(x => x.ParentCategory)
            .HasForeignKey(x => x.ParentCategoryId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(x => x.ProductBases)
            .WithOne(x => x.Category)
            .HasForeignKey(x => x.CategoryId);

        builder.HasMany(x => x.Translations)
            .WithOne(x => x.Category)
            .HasForeignKey(x => x.CategoryId);
    }
}