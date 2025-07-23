using Product.Domain.Aggregates.Categories;

namespace Product.Core.Dtos.Category;

public class CategoryEventDto(CategoryAggregate entity)
{
    private readonly CategoryAggregate _entity = entity;

    public Guid Id => _entity.Id;

    public string Name => _entity.Name;

    public Guid? ParentCategoryId => _entity.ParentCategoryId;

    public List<Guid> SubCategoryIds => _entity.SubCategories.Select(x => x.Id).ToList();
}