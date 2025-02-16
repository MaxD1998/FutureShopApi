using Shared.Core.Bases;
using Shop.Domain.Entities;
using System.Linq.Expressions;

namespace Shop.Core.Dtos;

public class IdNameDto : BaseIdNameDto
{
    public static Expression<Func<CategoryEntity, IdNameDto>> MapFromCategory() => MapFromCategory(null);

    public static Expression<Func<CategoryEntity, IdNameDto>> MapFromCategory(string lang) => entity => new IdNameDto
    {
        Id = entity.Id,
        Name = string.IsNullOrEmpty(lang)
            ? entity.Name
            : entity.Translations.AsQueryable().Where(x => x.Lang == lang).Select(x => x.Translation).FirstOrDefault() ?? entity.Name,
    };

    public static Expression<Func<ProductBaseEntity, IdNameDto>> MapFromProductBase() => entity => new IdNameDto
    {
        Id = entity.Id,
        Name = entity.Name,
    };
}