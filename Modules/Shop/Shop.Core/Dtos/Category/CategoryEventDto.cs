using Shop.Domain.Entities;
using Shop.Infrastructure;

namespace Shop.Core.Dtos.Category;

public class CategoryEventDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public Guid ParentId { get; set; }

    public List<CategoryEventDto> SubCategories { get; set; } = [];

    public CategoryEntity Map(ShopContext context) => new()
    {
        ExternalId = Id,
        Name = Name,
        ParentCategoryId = context.Set<CategoryEntity>().Where(x => x.ExternalId == ParentId).Select(x => (Guid?)x.Id).FirstOrDefault(),
        SubCategories = context.Set<CategoryEntity>().Where(x => SubCategories.Select(y => y.Id).Contains(x.ExternalId)).ToList()
    };
}