using Shared.Domain.Bases;

namespace Warehouse.Domain.Entities;

public class GoodsReceiptProductEntity : BaseEntity
{
    public Guid GoodsReceiptId { get; set; }

    public Guid ProductId { get; set; }

    public int Quantity { get; set; }

    #region Related Data

    public GoodsReceiptEntity GoodsReceipt { get; set; }

    public ProductEntity Product { get; set; }

    #endregion Related Data
}