﻿using Shared.Domain.Bases;

namespace Warehouse.Domain.Entities;

public class GoodsIssueProductsEntity : BaseEntity
{
    public Guid GoodsIssueId { get; set; }

    public Guid ProductId { get; set; }

    public int Quantity { get; set; }

    #region Related Data

    public GoodsIssueEntity GoodsIssue { get; set; }

    public ProductEntity Product { get; set; }

    #endregion Related Data
}