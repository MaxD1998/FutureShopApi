using FluentValidation;
using Product.Core.Dtos.PurchaseListItem;
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

    public Guid? Id { get; private set; }

    public bool IsFavourite { get; set; }

    public string Name { get; set; }

    public List<PurchaseListItemFormDto> PurchaseListItems { get; set; } = [];

    public PurchaseListEntity ToEntity() => new()
    {
        Id = Id ?? Guid.Empty,
        IsFavourite = IsFavourite,
        Name = Name,
        PurchaseListItems = PurchaseListItems.Select(x => x.ToEntity()).ToList()
    };
}

public class PurchaseListFormValidator : AbstractValidator<PurchaseListFormDto>
{
}