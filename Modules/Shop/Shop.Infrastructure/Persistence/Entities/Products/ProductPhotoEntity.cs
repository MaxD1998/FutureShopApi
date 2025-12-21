using Shared.Domain.Bases;
using Shared.Domain.Exceptions;
using Shared.Domain.Interfaces;

namespace Shop.Infrastructure.Persistence.Entities.Products;

public class ProductPhotoEntity : BaseExternalEntity, IUpdateEvent<ProductPhotoEntity>, IEntityValidation
{
    public string FileId { get; set; }

    public int Position { get; set; }

    public Guid ProductId { get; set; }

    #region Related Data

    public ProductEntity Product { get; set; }

    #endregion Related Data

    #region Methods

    public void UpdateEvent(ProductPhotoEntity entity)
    {
        Position = entity.Position;
    }

    public void Validate()
    {
        ValidateFileId();
        ValidatePosition();
    }

    private void ValidateFileId()
    {
        if (string.IsNullOrWhiteSpace(FileId))
            throw new PropertyWasEmptyException(nameof(FileId));
    }

    private void ValidatePosition()
    {
        if (Position < 0)
            throw new PropertyWasNegativeException(nameof(Position));
    }

    #endregion Methods
}