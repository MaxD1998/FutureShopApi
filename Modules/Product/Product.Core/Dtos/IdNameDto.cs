using Product.Domain.Entities;
using Shared.Core.Bases;

namespace Product.Core.Dtos;

public class IdNameDto : BaseIdNameDto
{
    public IdNameDto(CategoryEntity entity)
    {
        Id = entity.Id;
        Name = entity.Translations.FirstOrDefault()?.Translation ?? entity.Name;
    }
}