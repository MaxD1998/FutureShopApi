using Product.Domain.Aggregates.Categories;
using Shared.Core.Interfaces;
using System.Linq.Expressions;

namespace Product.Core.Dtos.Category;

public class CategoryListDto : IDto
{
    public Guid Id { get; private set; }

    public bool IsSubCategory { get; private set; }

    public string Name { get; private set; }

    public int SubCategoryQuantity { get; private set; }

    public static Expression<Func<CategoryAggregate, CategoryListDto>> Map() => entity => new CategoryListDto
    {
        SubCategoryQuantity = entity.SubCategories.Count,
        Id = entity.Id,
        Name = entity.Name,
        IsSubCategory = entity.ParentCategoryId.HasValue,
    };
}