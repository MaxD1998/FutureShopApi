﻿using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Constants;
using Shared.Infrastructure.Exceptions;
using Shared.Infrastructure.Extensions;
using Shared.Infrastructure.Interfaces;

namespace Shop.Infrastructure.Entities.PurchaseLists;

public class PurchaseListEntity : BaseEntity, IUpdate<PurchaseListEntity>, IEntityValidation
{
    public bool IsFavourite { get; set; }

    public string Name { get; set; }

    public Guid? UserId { get; set; }

    #region Related Data

    public ICollection<PurchaseListItemEntity> PurchaseListItems { get; set; } = [];

    #endregion Related Data

    #region Methods

    public void Update(PurchaseListEntity entity)
    {
        IsFavourite = entity.IsFavourite;
        Name = entity.Name;
        PurchaseListItems.UpdateEntities(entity.PurchaseListItems);
    }

    public void Validate()
    {
        ValidateName();

        PurchaseListItems.ValidateEntities();
    }

    private void ValidateName()
    {
        if (IsFavourite)
            return;

        if (string.IsNullOrWhiteSpace(Name))
            throw new PropertyWasEmptyException(nameof(Name));

        var length = StringLengthConst.LongString;

        if (Name.Length > length)
            throw new PropertyWasTooLongException(nameof(Name), length);
    }

    #endregion Methods
}