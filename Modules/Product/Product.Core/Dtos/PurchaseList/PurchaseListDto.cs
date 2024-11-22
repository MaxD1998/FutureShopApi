using Product.Core.Dtos.PurchaseListItem;
using Product.Domain.Entities;

namespace Product.Core.Dtos.PurchaseList;

public class PurchaseListDto
{
    public PurchaseListDto(PurchaseListEntity entity)
    {
        Id = entity.Id;
        IsFavourite = entity.IsFavourite;
        LastUpdateDate = DateOnly.FromDateTime(entity.ModifyTime ?? entity.CreateTime);
        Name = entity.Name;
        PurchaseListItems = entity.PurchaseListItems.Select(x => new PurchaseListItemDto(x));
        UserId = entity.UserId;
    }

    public Guid Id { get; set; }

    public bool IsFavourite { get; set; }

    public DateOnly LastUpdateDate { get; set; }

    public string Name { get; set; }

    public IEnumerable<PurchaseListItemDto> PurchaseListItems { get; set; }

    public Guid? UserId { get; set; }
}