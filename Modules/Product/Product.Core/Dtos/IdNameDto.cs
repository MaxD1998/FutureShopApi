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
        Name = string.IsNullOrEmpty(lang)
            ? entity.Name
            : entity.Translations.AsQueryable().Where(x => x.Lang == lang).Select(x => x.Translation).FirstOrDefault() ?? entity.Name,
    };

    public static Expression<Func<ProductBaseEntity, IdNameDto>> MapFromProductBase() => entity => new IdNameDto
    {
        Id = entity.Id,
        Name = entity.Name,
    };

    public static Expression<Func<ProductParameterEntity, IdNameDto>> MapFromProductParameter(string lang) => entity => new IdNameDto
    {
        Id = entity.Id,
        Name = entity.Translations.AsQueryable().Where(x => x.Lang == lang).Select(x => x.Translation).FirstOrDefault() ?? entity.Name,
    };
}