using Shared.Core.Bases;
using Shop.Domain.Entities;
using System.Linq.Expressions;

namespace Shop.Core.Dtos;

public class IdNameValueDto : BaseIdNameValueDto
{
    public static Expression<Func<ProductParameterValueEntity, IdNameValueDto>> MapFromProductParameterValue(string lang) => entity => new()
    {
        Id = entity.Id,
        Name = entity.ProductParameter.Translations.AsQueryable().Where(x => x.Lang == lang).Select(x => x.Translation).FirstOrDefault() ?? entity.ProductParameter.Name,
        Value = entity.Value
    };
}