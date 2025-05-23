using Shared.Domain.Bases;
using Shared.Domain.Interfaces;

namespace Shop.Domain.Entities;

public class PriceEntity : BaseEntity, IUpdate<PriceEntity>
{
    public DateTime? End { get; set; }

    public decimal Price { get; set; }

    public Guid ProductId { get; set; }

    public DateTime? Start { get; set; }

    #region Related Data

    public ProductEntity Product { get; set; }

    public void Update(PriceEntity entity)
    {
        End = entity.End;
        Price = entity.Price;
        ProductId = entity.ProductId;
        Start = entity.Start;
    }

    #endregion Related Data
}