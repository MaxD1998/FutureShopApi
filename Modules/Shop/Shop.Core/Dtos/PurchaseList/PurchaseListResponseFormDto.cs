using Shop.Domain.Entities.PurchaseLists;
using System.Linq.Expressions;

namespace Shop.Core.Dtos.PurchaseList;

public class PurchaseListResponseFormDto : PurchaseListRequestFormDto
{
    public Guid Id { get; set; }

    public static Expression<Func<PurchaseListEntity, PurchaseListResponseFormDto>> Map() => entity => new()
    {
        Id = entity.Id,
        IsFavourite = entity.IsFavourite,
        Name = entity.Name,
    };
}