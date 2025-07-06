using Shared.Domain.Bases;
using Shared.Domain.Interfaces;

namespace Shop.Domain.Entities;

public class PriceEntity : BaseEntity, IUpdate<PriceEntity>, IEquatable<PriceEntity>
{
    public DateTime? End { get; set; }

    public decimal Price { get; set; }

    public Guid ProductId { get; set; }

    public DateTime? Start { get; set; }

    #region Related Data

    public ProductEntity Product { get; set; }

    #endregion Related Data

    public bool Equals(PriceEntity other)
    {
        if (other == null)
            return false;

        return Id == other.Id && End == other.End && Price == other.Price && Start == other.Start;
    }

    public override int GetHashCode() => HashCode.Combine(Id, Start, End, Price);

    public void Update(PriceEntity entity)
    {
        End = entity.End;
        Price = entity.Price;
        Start = entity.Start;
    }
}