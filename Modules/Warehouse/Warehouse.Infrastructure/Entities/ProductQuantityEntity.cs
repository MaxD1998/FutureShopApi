using Shared.Domain.Bases;

namespace Warehouse.Infrastructure.Entities;

public class ProductQuantityEntity : BaseEntity
{
    public Guid ProductId { get; set; }

    public Guid WarehouseId { get; set; }

    #region Realted Data

    public ProductEntity Product { get; set; }

    public WarehouseEntity Warehouse { get; set; }

    #endregion Realted Data
}