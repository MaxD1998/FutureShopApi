using Shop.Infrastructure.Persistence.Entities.Products;
using System.Linq.Expressions;

namespace Shop.Core.Dtos.Product.Price;

public class PriceFormDto
{
    public DateTime? End { get; set; }

    public Guid? Id { get; set; }

    public decimal Price { get; set; }

    public DateTime? Start { get; set; }

    public static Expression<Func<PriceEntity, PriceFormDto>> Map() => entity => new()
    {
        End = entity.End,
        Id = entity.Id,
        Price = entity.Price,
        Start = entity.Start,
    };

    public PriceEntity ToEntity() => new()
    {
        End = End,
        Id = Id ?? Guid.Empty,
        Price = Price,
        Start = Start,
    };
}