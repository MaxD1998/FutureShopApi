using Shared.Core.Interfaces;
using Shop.Domain.Entities.Categories;
using System.Linq.Expressions;

namespace Shop.Core.Dtos.Category;

public class CategoryListDto : IDto
{
    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public Guid? ParentCategoryId { get; private set; }

    public IEnumerable<CategoryListDto> SubCategories { get; private set; }

    public static List<CategoryListDto> BuildTree(List<CategoryListDto> dtos)
    {
        var categories = dtos.ToList();
        categories.ForEach(category => category.SubCategories = categories.Where(x => x.ParentCategoryId == category.Id).ToList());

        return categories.Where(x => x.ParentCategoryId == null).ToList();
    }

    public static Expression<Func<CategoryEntity, CategoryListDto>> Map(string lang) => entity => new CategoryListDto
    {
        Id = entity.Id,
        Name = entity.Translations.Where(x => x.Lang == lang).Select(x => x.Translation).FirstOrDefault() ?? entity.Name,
        ParentCategoryId = entity.ParentCategoryId,
    };
}