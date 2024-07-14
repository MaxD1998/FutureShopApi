using Product.Domain.Entities;
using Shared.Core.Interfaces;

namespace Product.Core.Dtos.Category;

public class CategoryListDto : IDto
{
    public CategoryListDto(CategoryEntity entity)
    {
        HasSubCategories = entity.SubCategories.Count != 0;
        Id = entity.Id;
        Name = entity.Translations.FirstOrDefault()?.Translation ?? entity.Name;
        ParentCategoryId = entity.ParentCategoryId;
    }

    public bool HasSubCategories { get; }

    public Guid Id { get; }

    public string Name { get; }

    public Guid? ParentCategoryId { get; }
}