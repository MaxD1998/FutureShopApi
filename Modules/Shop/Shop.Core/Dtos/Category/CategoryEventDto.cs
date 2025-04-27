using Shop.Domain.Entities;
using Shop.Infrastructure;

namespace Shop.Core.Dtos.Category;

public class CategoryEventDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public Guid? ParentCategoryId { get; set; }

    public List<CategoryEventDto> SubCategories { get; set; } = [];

    public CategoryEntity Map(ShopPostgreSqlContext context) => new()
    {
        ExternalId = Id,
        Name = Name,
        ParentCategoryId = context.Set<CategoryEntity>().Where(x => x.ExternalId == ParentCategoryId).Select(x => (Guid?)x.Id).FirstOrDefault(),
        SubCategories = context.Set<CategoryEntity>().Where(x => SubCategories.Select(y => y.Id).Contains(x.ExternalId)).ToList()
    };
}