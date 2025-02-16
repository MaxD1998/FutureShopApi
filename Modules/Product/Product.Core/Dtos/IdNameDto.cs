using Product.Domain.Entities;
using Shared.Core.Bases;
using System.Linq.Expressions;

namespace Product.Core.Dtos;

public class IdNameDto : BaseIdNameDto
{
    public static Expression<Func<CategoryEntity, IdNameDto>> MapFromCategory() => MapFromCategory(null);

    public static Expression<Func<CategoryEntity, IdNameDto>> MapFromCategory(string lang) => entity => new IdNameDto
    {
        Id = entity.Id,
        Name = entity.Name
    };

    public static Expression<Func<ProductBaseEntity, IdNameDto>> MapFromProductBase() => entity => new IdNameDto
    {
        Id = entity.Id,
        Name = entity.Name,
    };
}