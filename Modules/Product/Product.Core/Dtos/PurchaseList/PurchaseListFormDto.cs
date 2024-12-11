using FluentValidation;
using Product.Core.Dtos.PurchaseListItem;
using Product.Domain.Entities;
using System.Linq.Expressions;

namespace Product.Core.Dtos.PurchaseList;

public class PurchaseListFormDto
{
    public Guid? Id { get; private set; }

    public bool IsFavourite { get; set; }

    public string Name { get; set; }

    public List<PurchaseListItemFormDto> PurchaseListItems { get; set; } = [];

    public static Expression<Func<PurchaseListEntity, PurchaseListFormDto>> Map() => entity => new()
    {
        Id = entity.Id,
        IsFavourite = entity.IsFavourite,
        Name = entity.Name,
    };

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