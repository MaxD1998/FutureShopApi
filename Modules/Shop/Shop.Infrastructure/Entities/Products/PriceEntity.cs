﻿using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Exceptions;
using Shared.Infrastructure.Interfaces;
using Shop.Infrastructure.Exceptions.AdCampaigns;

namespace Shop.Infrastructure.Entities.Products;

public class PriceEntity : BaseEntity, IUpdate<PriceEntity>, IEquatable<PriceEntity>, IEntityValidation
{
    public DateTime? End { get; set; }

    public decimal Price { get; set; }

    public Guid ProductId { get; private set; }

    public DateTime? Start { get; set; }

    #region Related Data

    public ProductEntity Product { get; private set; }

    #endregion Related Data

    #region Methods

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

    public void Validate()
    {
        ValidatePrice();
        ValidateStartEnd();
    }

    private void ValidatePrice()
    {
        if (Price < 0)
            throw new PropertyWasNegativeException(nameof(Price));
    }

    private void ValidateStartEnd()
    {
        if (Start.HasValue && End.HasValue)
        {
            if (End < Start)
                throw new InvalidDateRangeException(Start.Value, End.Value);
        }
    }

    #endregion Methods
}