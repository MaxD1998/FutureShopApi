using Product.Domain.Entities;
using Shared.Core.Bases;
using System.Linq.Expressions;

namespace Product.Core.Dtos;

public class IdNameValueDto : BaseIdNameValueDto
{
    public static Expression<Func<ProductParameterValueEntity, IdNameValueDto>> MapFromProductParameterValue(string lang) => entity => new()
    {
        Id = entity.Id,
        Name = entity.ProductParameter.Translations.AsQueryable().Where(x => x.Lang == lang).Select(x => x.Translation).FirstOrDefault() ?? entity.ProductParameter.Name,
        Value = entity.Value
    };
}