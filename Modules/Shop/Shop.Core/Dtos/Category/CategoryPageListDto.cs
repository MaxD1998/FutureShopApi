using Shop.Infrastructure.Entities.Categories;
using System.Linq.Expressions;

namespace Shop.Core.Dtos.Category;

public class CategoryPageListDto
{
    public Guid Id { get; private set; }

    public bool IsSubCategory { get; private set; }

    public string Name { get; private set; }

    public int SubCategoryQuantity { get; private set; }

    public static Expression<Func<CategoryEntity, CategoryPageListDto>> Map() => entity => new CategoryPageListDto
    {
        Id = entity.Id,
        Name = entity.Name,
        IsSubCategory = entity.ParentCategoryId.HasValue,
        SubCategoryQuantity = entity.SubCategories.AsQueryable().Count(),
    };
}