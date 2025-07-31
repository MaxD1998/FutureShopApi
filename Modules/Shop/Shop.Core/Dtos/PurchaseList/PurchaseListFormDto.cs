using Shop.Core.Dtos.PurchaseListItem;
using Shop.Domain.Aggregates.PurchaseLists;
using System.Linq.Expressions;

namespace Shop.Core.Dtos.PurchaseList;

public class PurchaseListFormDto
{
    public Guid? Id { get; private set; }

    public bool IsFavourite { get; set; }

    public string Name { get; set; }

    public List<PurchaseListItemFormDto> PurchaseListItems { get; set; } = [];

    public static Expression<Func<PurchaseListAggregate, PurchaseListFormDto>> Map() => entity => new()
    {
        Id = entity.Id,
        IsFavourite = entity.IsFavourite,
        Name = entity.Name,
    };

    public PurchaseListAggregate ToEntity(Guid? userId) => new()
    {
        Id = Id ?? Guid.Empty,
        IsFavourite = IsFavourite,
        Name = Name,
        PurchaseListItems = PurchaseListItems.Select(x => x.ToEntity()).ToList(),
        UserId = userId
    };
}