using Shared.Core.Bases;
using Shop.Domain.Aggregates.Categories;
using Shop.Domain.Aggregates.ProductBases;
using System.Linq.Expressions;

namespace Shop.Core.Dtos;

public class IdNameDto : BaseIdNameDto
{
    public static Expression<Func<CategoryAggregate, IdNameDto>> MapFromCategory() => MapFromCategory(null);

    public static Expression<Func<CategoryAggregate, IdNameDto>> MapFromCategory(string lang) => entity => new IdNameDto
    {
        Id = entity.Id,
        Name = string.IsNullOrEmpty(lang)
            ? entity.Name
            : entity.Translations.AsQueryable().Where(x => x.Lang == lang).Select(x => x.Translation).FirstOrDefault() ?? entity.Name,
    };

    public static Expression<Func<ProductBaseAggregate, IdNameDto>> MapFromProductBase() => entity => new IdNameDto
    {
        Id = entity.Id,
        Name = entity.Name,
    };
}