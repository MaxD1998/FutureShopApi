using Shared.Domain.Bases;
using Warehouse.Domain.Enums;

namespace Warehouse.Domain.Entities;

public class GoodsIssueEntity : BaseEntity
{
    public GoodsIssueType Type { get; set; }

    public Guid WarehouseId { get; set; }

    #region Related Data

    public ICollection<GoodsIssueProductsEntity> GoodsIssueProducts { get; set; } = [];

    public WarehouseEntity Warehouse { get; set; }

    #endregion Related Data
}