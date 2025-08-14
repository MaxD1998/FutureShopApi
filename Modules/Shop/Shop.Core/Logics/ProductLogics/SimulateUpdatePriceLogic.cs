using Shop.Infrastructure.DomainLogics;
using Shop.Infrastructure.Entities;
using Shop.Infrastructure.Repositories;

namespace Shop.Core.Logics.ProductLogics;

internal class SimulateUpdatePriceLogic(IProductRepository productRepository) : BaseSimulatePriceActionLogic(productRepository)
{
    protected override Action<ICollection<PriceEntity>, PriceEntity, DateTime, bool> Action => PriceDomainLogic.Update;
}