using Shared.Domain.Bases;
using Shared.Domain.Interfaces;

namespace Product.Domain.Aggregates.Products.Entities;

public class ProductPhoto : BaseEntity, IUpdate<ProductPhoto>
{
    public string FileId { get; set; }

    public int Position { get; set; }

    public Guid ProductId { get; set; }

    #region Related Data

    public ProductAggregate Product { get; set; }

    #endregion Related Data

    public void Update(ProductPhoto entity)
    {
        Position = entity.Position;
    }
}