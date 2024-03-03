using Product.Domain.Entities;
using Shared.Core.Interfaces;

namespace Product.Core.Dtos.Category;

public class CategoryDto : IDto
{
    public CategoryDto()
    {
    }

    public CategoryDto(CategoryEntity entity)
    {
        HasChildren = entity.SubCategories.Any();
        Id = entity.Id;
        Name = entity.Translations.FirstOrDefault()?.Translation ?? entity.Name;
        ParentCategoryId = entity.ParentCategoryId;
    }

    public bool HasChildren { get; set; }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public Guid? ParentCategoryId { get; set; }
}