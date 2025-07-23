using System.Linq.Expressions;

namespace Product.Core.Dtos.Category;

public class CategoryFormDto
{
    public string Name { get; set; }

    public Guid? ParentCategoryId { get; set; }

    public List<IdNameDto> SubCategories { get; set; } = [];

    public static Expression<Func<Domain.Aggregates.Categories.CategoryAggregate, CategoryFormDto>> Map() => entity => new CategoryFormDto
    {
        Name = entity.Name,
        ParentCategoryId = entity.ParentCategoryId,
        SubCategories = entity.SubCategories.AsQueryable().Select(IdNameDto.MapFromCategory()).ToList(),
    };

    public Domain.Aggregates.Categories.CategoryAggregate ToEntity() => new(Name)
    {
        Name = Name,
        ParentCategoryId = ParentCategoryId,
    };
}