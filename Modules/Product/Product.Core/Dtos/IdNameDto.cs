using Shared.Core.Bases;
using System.Linq.Expressions;

namespace Product.Core.Dtos;

public class IdNameDto : BaseIdNameDto
{
    public static Expression<Func<Domain.Aggregates.Categories.CategoryAggregate, IdNameDto>> MapFromCategory() => MapFromCategory(null);

    public static Expression<Func<Domain.Aggregates.Categories.CategoryAggregate, IdNameDto>> MapFromCategory(string lang) => entity => new IdNameDto
    {
        Id = entity.Id,
        Name = entity.Name
    };

    public static Expression<Func<Domain.Aggregates.ProductBases.ProductBaseAggregate, IdNameDto>> MapFromProductBase() => entity => new IdNameDto
    {
        Id = entity.Id,
        Name = entity.Name,
    };
}