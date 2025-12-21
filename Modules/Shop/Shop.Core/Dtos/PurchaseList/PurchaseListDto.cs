using Shop.Core.Dtos.PurchaseList.PurchaseListItem;
using Shop.Domain.Entities.PurchaseLists;
using System.Linq.Expressions;

namespace Shop.Core.Dtos.PurchaseList;

public class PurchaseListDto
{
    public Guid Id { get; set; }

    public bool IsFavourite { get; set; }

    public DateOnly LastUpdateDate { get; set; }

    public string Name { get; set; }

    public IEnumerable<PurchaseListItemDto> PurchaseListItems { get; set; }

    public Guid? UserId { get; set; }

    public static Expression<Func<PurchaseListEntity, PurchaseListDto>> Map() => entity => new()
    {
        Id = entity.Id,
        IsFavourite = entity.IsFavourite,
        LastUpdateDate = DateOnly.FromDateTime(entity.ModifyTime ?? entity.CreateTime),
        Name = entity.Name,
        PurchaseListItems = entity.PurchaseListItems.AsQueryable().Select(PurchaseListItemDto.Map()).ToList(),
        UserId = entity.UserId,
    };
}