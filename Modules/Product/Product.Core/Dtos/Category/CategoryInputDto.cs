using Shared.Core.Interfaces;

namespace Product.Core.Dtos.Category;

public class CategoryInputDto : IInputDto
{
    public string Name { get; set; }

    public Guid? ParentCategoryId { get; set; }
}