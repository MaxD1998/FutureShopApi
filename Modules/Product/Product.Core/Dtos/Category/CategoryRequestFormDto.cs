using Product.Domain.Entities;

namespace Product.Core.Dtos.Category;

public class CategoryRequestFormDto
{
    public string Name { get; set; }

    public Guid? ParentCategoryId { get; set; }

    public List<IdNameDto> SubCategories { get; set; } = [];

    public CategoryEntity ToEntity() => new()
    {
        Name = Name,
        ParentCategoryId = ParentCategoryId,
    };
}