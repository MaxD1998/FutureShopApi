using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Entities;
using Shared.Infrastructure.Bases;
using Shared.Shared.Constants;

namespace Product.Infrastructure.Configurations;

public class CategoryConfig : BaseConfig<CategoryEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<CategoryEntity> builder)
    {
        builder.Property(x => x.Name)
            .HasMaxLength(StringLengthConst.LongString)
            .HasColumnOrder(100)
            .IsRequired();

        builder.HasMany(x => x.SubCategories)
            .WithOne(x => x.ParentCategory)
            .HasForeignKey(x => x.ParentCategoryId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(x => x.ProductBases)
            .WithOne(x => x.Category)
            .HasForeignKey(x => x.CategoryId);
    }
}