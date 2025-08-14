using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Interfaces;

namespace Warehouse.Infrastructure.Entities;

public class ProductEntity : BaseEntity, IUpdate<ProductEntity>
{
    public string Name { get; set; }

    #region Related Data

    public ICollection<GoodsIssueProductsEntity> GoodsIssueProducts { get; set; } = [];

    public ICollection<GoodsReceiptProductEntity> GoodsReceiptProducts { get; set; } = [];

    public ICollection<InterWarehouseTransferProductsEntity> InterWarehouseTransferProducts { get; set; } = [];

    public ICollection<ProductQuantityEntity> ProductQuantities { get; set; } = [];

    #endregion Related Data

    public void Update(ProductEntity entity)
    {
        Name = entity.Name;
    }
}