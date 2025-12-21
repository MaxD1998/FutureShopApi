using Shared.Domain.Bases;

namespace Warehouse.Infrastructure.Entities;

public class InterWarehouseTransferProductsEntity : BaseEntity
{
    public Guid InterWarehouseTrasferId { get; set; }

    public Guid ProductId { get; set; }

    public int Quantity { get; set; }

    #region Related Data

    public InterWarehouseTransferEntity InterWarehouseTransfer { get; set; }

    public ProductEntity Product { get; set; }

    #endregion Related Data
}