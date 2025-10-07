﻿using Shared.Infrastructure.Bases;

namespace Shop.Infrastructure.Entities.ProductBases;

public class ProductParameterTranslationEntity : BaseTranslationEntity<ProductParameterTranslationEntity>
{
    public Guid ProductParameterId { get; private set; }

    #region Related Data

    public ProductParameterEntity ProductParameter { get; private set; }

    #endregion Related Data
}