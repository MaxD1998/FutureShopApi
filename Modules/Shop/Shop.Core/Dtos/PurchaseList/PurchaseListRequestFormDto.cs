using Shop.Core.Dtos.PurchaseList.PurchaseListItem;
using Shop.Domain.Entities.PurchaseLists;

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