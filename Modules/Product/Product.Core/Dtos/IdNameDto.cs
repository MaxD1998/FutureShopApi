using Product.Domain.Entities;
using Shared.Core.Bases;

namespace Product.Core.Dtos;

public class IdNameDto : BaseIdNameDto
{
    public IdNameDto() : base()
    {
    }

    public IdNameDto(CategoryEntity entity) : base(entity.Id, entity.Translations?.FirstOrDefault()?.Translation ?? entity.Name)
    {
    }

    public IdNameDto(ProductBaseEntity entity) : base(entity.Id, entity.Name)
    {
    }

    public IdNameDto(ProductParameterEntity entity) : base(entity.Id, entity.Translations?.FirstOrDefault()?.Translation ?? entity.Name)
    {
    }
}