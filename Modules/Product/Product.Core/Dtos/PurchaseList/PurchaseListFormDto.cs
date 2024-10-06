using Product.Domain.Entities;

namespace Product.Core.Dtos.PurchaseList;

public class PurchaseListFormDto
{
    public PurchaseListFormDto()
    {
    }

    public PurchaseListFormDto(PurchaseListEntity entity)
    {
        Id = entity.Id;
        IsFavourite = entity.IsFavourite;
        Name = entity.Name;
    }

    public Guid Id { get; private set; }

    public bool IsFavourite { get; set; }

    public string Name { get; set; }

    public PurchaseListEntity ToEntity() => new()
    {
        IsFavourite = IsFavourite,
        Name = Name,
    };
}