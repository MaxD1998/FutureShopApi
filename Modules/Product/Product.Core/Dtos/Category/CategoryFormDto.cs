using Product.Domain.Aggregates.Categories;
using System.Linq.Expressions;

namespace Product.Core.Dtos.Category;

public class CategoryFormDto
{
    public string Name { get; set; }

    public Guid? ParentCategoryId { get; set; }

    public List<IdNameDto> SubCategories { get; set; } = [];

    public static Expression<Func<CategoryAggregate, CategoryFormDto>> Map() => entity => new CategoryFormDto
    {
        Name = entity.Name,
        ParentCategoryId = entity.ParentCategoryId,
        SubCategories = entity.SubCategories.AsQueryable().Select(IdNameDto.MapFromCategory()).ToList(),
    };

    public CategoryAggregate ToEntity() => new(Name, ParentCategoryId);
}