using Shared.Domain.Bases;

namespace Warehouse.Domain.Entities;

public class InterWarehouseTransferEntity : BaseEntity
{
    public DateTime DateOfReceipt { get; set; }

    public Guid DestinationWarehouseId { get; set; }

    public Guid SourceWarehouseId { get; set; }

    #region Related Data

    public WarehouseEntity DestinationWarehouse { get; set; }

    public ICollection<InterWarehouseTransferProductsEntity> InterWarehouseTransferProducts { get; set; } = [];

    public WarehouseEntity SourceWarehouse { get; set; }

    #endregion Related Data
}