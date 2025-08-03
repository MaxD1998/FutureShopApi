using Shared.Domain.Bases;
using Shared.Domain.Interfaces;

namespace Product.Domain.Aggregates.Products.Entities;

public class ProductPhotoEntity : BaseEntity, IUpdate<ProductPhotoEntity>
{
    public ProductPhotoEntity()
    {
    }

    public ProductPhotoEntity(Guid id, string fileId, int position)
    {
        Id = id;
        SetFileId(fileId);
        SetPosition(position);
    }

    public string FileId { get; private set; }

    public int Position { get; private set; }

    public Guid ProductId { get; private set; }

    #region Setters

    private void SetFileId(string fileId)
    {
        ValidateRequiredProperty(nameof(fileId), fileId);

        FileId = fileId;
    }

    private void SetPosition(int position)
    {
        ValidateNonNegativeProperty(nameof(Position), position);

        Position = position;
    }

    #endregion Setters

    #region Methods

    public void Update(ProductPhotoEntity entity)
    {
        Position = entity.Position;
    }

    #endregion Methods
}