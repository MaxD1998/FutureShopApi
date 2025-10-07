﻿using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Exceptions;
using Shared.Infrastructure.Interfaces;

namespace Product.Infrastructure.Entities;

public class ProductPhotoEntity : BaseEntity, IUpdate<ProductPhotoEntity>, IEntityValidation
{
    public string FileId { get; set; }

    public int Position { get; set; }

    public Guid ProductId { get; private set; }

    #region Related Data

    public ProductEntity Product { get; private set; }

    #endregion Related Data

    #region Methods

    public void Update(ProductPhotoEntity entity)
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