using Shop.Infrastructure.Entities;
using Shop.Infrastructure.Helpers;
using Shop.Infrastructure.Repositories;

namespace Shop.Core.Logics.ProductLogics;

internal class SimulateUpdatePriceLogic(IProductRepository productRepository) : BaseSimulatePriceActionLogic(productRepository)
{
    protected override Action<ICollection<PriceEntity>, PriceEntity, DateTime, bool> Action => PriceHelper.Update;
}