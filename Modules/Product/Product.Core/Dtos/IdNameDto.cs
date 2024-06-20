using Product.Domain.Entities;
using Shared.Core.Bases;

namespace Product.Core.Dtos;

public class IdNameDto : BaseIdNameDto
{
    public IdNameDto(CategoryEntity entity) : base(entity.Id, entity.Translations.FirstOrDefault()?.Translation ?? entity.Name)
    {
    }
}