using Shared.Domain.Bases;

namespace Product.Domain.Entities;

public class ProductPhotoEntity : BaseEntity
{
    public string FileId { get; set; }

    public int Position { get; set; }

    public Guid ProductId { get; set; }

    #region Related Data

    public ProductEntity Product { get; set; }

    #endregion Related Data
}