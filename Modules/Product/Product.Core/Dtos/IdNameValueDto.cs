using Product.Domain.Entities;
using Shared.Core.Bases;

namespace Product.Core.Dtos;

public class IdNameValueDto : BaseIdNameValueDto
{
    public IdNameValueDto(ProductParameterValueEntity entity)
        : base(entity.Id, entity.ProductParameter.Translations.FirstOrDefault()?.Translation ?? entity.ProductParameter.Name, entity.Value)
    {
    }
}