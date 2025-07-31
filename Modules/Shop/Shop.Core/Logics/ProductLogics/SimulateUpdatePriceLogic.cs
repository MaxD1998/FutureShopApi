using Shop.Core.Interfaces;
using Shop.Domain.Aggregates.Products.Entities;
using Shop.Domain.Aggregates.Products.Logics;

namespace Shop.Core.Logics.ProductLogics;

internal class SimulateUpdatePriceLogic(ILogic<Guid?, bool> getProductWasActiveLogic) : BaseSimulatePriceActionLogic(getProductWasActiveLogic)
{
    protected override Action<ICollection<PriceEntity>, PriceEntity, DateTime, bool> Action => ProductLogic.Update;
}