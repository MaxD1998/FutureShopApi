using Shared.Domain.Bases;
using Shared.Domain.Interfaces;
using Warehouse.Infrastructure.Enums;

namespace Warehouse.Infrastructure.Entities;

public class WarehouseEntity : BaseEntity, IUpdate<WarehouseEntity>
{
    public string BuildingName { get; set; }

    public string City { get; set; }

    public string FlatNumber { get; set; }

    public bool IsActive { get; set; }

    public string Name { get; set; }

    public string PostalCode { get; set; }

    public string Street { get; set; }

    public WarehouseType Type { get; set; }

    #region Related Data

    public ICollection<GoodsIssueEntity> GoodsIssues { get; set; } = [];

    public ICollection<GoodsReceiptEntity> GoodsReceipts { get; set; } = [];

    public ICollection<InterWarehouseTransferEntity> IncomingTransfers { get; set; } = [];

    public ICollection<InterWarehouseTransferEntity> OutgoingTransfers { get; set; } = [];

    public ICollection<ProductQuantityEntity> ProductQuantities { get; set; } = [];

    #endregion Related Data

    public void Update(WarehouseEntity entity)
    {
        BuildingName = entity.BuildingName;
        City = entity.City;
        FlatNumber = entity.FlatNumber;
        Name = entity.Name;
        PostalCode = entity.PostalCode;
        Street = entity.Street;
        Type = entity.Type;
    }
}