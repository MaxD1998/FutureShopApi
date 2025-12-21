using Shop.Core.Interfaces.Repositories;
using Shop.Domain.DomainLogics;
using Shop.Domain.Entities.Products;

namespace Shop.Core.Logics.ProductLogics;

internal class SimulateUpdatePriceLogic(IProductRepository productRepository) : BaseSimulatePriceActionLogic(productRepository)
{
    protected override Action<ICollection<PriceEntity>, PriceEntity, DateTime, bool> Action => PriceDomainLogic.Update;
}