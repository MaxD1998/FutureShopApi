using Product.Core.Dtos.PurchaseListItem;
using Product.Domain.Entities;

namespace Product.Core.Dtos.PurchaseList;

public class PurchaseListDto
{
    public PurchaseListDto(PurchaseListEntity entity)
    {
        Id = entity.Id;
        Name = entity.Name;
        PurchaseListItems = entity.PurchaseListItems.Select(x => new PurchaseListItemDto(x));
        UserId = entity.UserId;
    }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public IEnumerable<PurchaseListItemDto> PurchaseListItems { get; set; }

    public Guid? UserId { get; set; }
}