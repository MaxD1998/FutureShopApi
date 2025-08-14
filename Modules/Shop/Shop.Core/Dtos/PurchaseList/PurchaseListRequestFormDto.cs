using Shop.Core.Dtos.PurchaseListItem;
using Shop.Infrastructure.Entities;

namespace Shop.Core.Dtos.PurchaseList;

public class PurchaseListRequestFormDto
{
    public bool IsFavourite { get; set; }

    public string Name { get; set; }

    public List<PurchaseListItemFormDto> PurchaseListItems { get; set; } = [];

    public PurchaseListEntity ToEntity(Guid? userId) => new()
    {
        IsFavourite = IsFavourite,
        Name = Name,
        PurchaseListItems = PurchaseListItems.Select(x => x.ToEntity()).ToList(),
        UserId = userId
    };
}