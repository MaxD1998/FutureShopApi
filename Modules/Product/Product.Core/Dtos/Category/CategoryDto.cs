using Shared.Core.Interfaces;

namespace Product.Core.Dtos.Category;

public class CategoryDto : CategoryInputDto, IDto
{
    public Guid Id { get; set; }

    public bool HasChildren { get; set; }
}