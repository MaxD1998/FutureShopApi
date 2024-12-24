using Shared.Domain.Bases;

namespace Warehouse.Domain.Entities;

public class GoodsReceiptEntity : BaseEntity
{
    public Guid WarehouseId { get; set; }

    #region Related Data

    public ICollection<GoodsReceiptProductEntity> GoodsReceiptProducts { get; set; } = [];

    public WarehouseEntity Warehouse { get; set; }

    #endregion Related Data
}