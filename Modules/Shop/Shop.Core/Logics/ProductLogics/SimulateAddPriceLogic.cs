using Shop.Infrastructure.Persistence.DomainLogics;
using Shop.Infrastructure.Persistence.Entities.Products;
using Shop.Infrastructure.Persistence.Repositories;

namespace Shop.Core.Logics.ProductLogics;

internal class SimulateAddPriceLogic(IProductRepository productRepository) : BaseSimulatePriceActionLogic(productRepository)
{
    protected override Action<ICollection<PriceEntity>, PriceEntity, DateTime, bool> Action => PriceDomainLogic.Add;
}