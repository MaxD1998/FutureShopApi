using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Aggregates.Categories;
using Product.Domain.Aggregates.ProductBases;
using Shared.Infrastructure.Bases;
using Shared.Shared.Constants;

namespace Product.Infrastructure.Configurations;

public class CategoryConfig : BaseConfig<CategoryAggregate>
{
    protected override void ConfigureEntity(EntityTypeBuilder<CategoryAggregate> builder)
    {
        builder.Property(x => x.Name)
            .HasMaxLength(StringLengthConst.LongString)
            .HasColumnOrder(100)
            .IsRequired();

        builder.Metadata.FindNavigation(nameof(CategoryAggregate.SubCategories)).SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.HasMany<ProductBaseAggregate>()
            .WithOne(x => x.Category)
            .HasForeignKey(x => x.CategoryId);

        builder.HasMany(x => x.SubCategories)
            .WithOne()
            .HasForeignKey(x => x.ParentCategoryId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}