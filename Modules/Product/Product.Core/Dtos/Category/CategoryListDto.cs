using Product.Domain.Entities;
using Shared.Core.Interfaces;
using System.Linq.Expressions;

namespace Product.Core.Dtos.Category;

public class CategoryListDto : IDto
{
    public bool HasSubCategories { get; private set; }

    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public Guid? ParentCategoryId { get; private set; }

    public static Expression<Func<CategoryEntity, CategoryListDto>> Map(string lang) => entity => new CategoryListDto
    {
        HasSubCategories = entity.SubCategories.Count != 0,
        Id = entity.Id,
        Name = entity.Translations.Where(x => x.Lang == lang).Select(x => x.Translation).FirstOrDefault() ?? entity.Name,
        ParentCategoryId = entity.ParentCategoryId,
    };
}