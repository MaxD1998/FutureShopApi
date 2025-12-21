using Product.Domain.Entities;
using System.Linq.Expressions;

namespace Product.Core.Dtos.Category;

public class CategoryResponseFormDto : CategoryRequestFormDto
{
    public Guid Id { get; set; }

    public static Expression<Func<CategoryEntity, CategoryResponseFormDto>> Map() => entity => new()
    {
        Id = entity.Id,
        Name = entity.Name,
        ParentCategoryId = entity.ParentCategoryId,
        SubCategories = entity.SubCategories.AsQueryable().Select(IdNameDto.MapFromCategory()).ToList(),
    };
}