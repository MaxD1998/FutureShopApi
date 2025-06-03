using Product.Domain.Entities;
using System.Linq.Expressions;

namespace Product.Core.Dtos.Category;

public class CategoryFormDto
{
    public string Name { get; set; }

    public Guid? ParentCategoryId { get; set; }

    public List<IdNameDto> SubCategories { get; set; } = [];

    public static Expression<Func<CategoryEntity, CategoryFormDto>> Map() => entity => new CategoryFormDto
    {
        Name = entity.Name,
        ParentCategoryId = entity.ParentCategoryId,
        SubCategories = entity.SubCategories.AsQueryable().Select(IdNameDto.MapFromCategory()).ToList(),
    };

    public CategoryEntity ToEntity() => new()
    {
        Name = Name,
        ParentCategoryId = ParentCategoryId,
    };
}