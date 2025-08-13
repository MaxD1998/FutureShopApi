using Shop.Core.Interfaces;
using Shop.Infrastructure.Entities;
using Shop.Infrastructure.Logics;

namespace Shop.Core.Logics.ProductLogics;

internal class SimulateAddPriceLogic(ILogic<Guid?, bool> getProductWasActiveLogic) : BaseSimulatePriceActionLogic(getProductWasActiveLogic)
{
    protected override Action<ICollection<PriceEntity>, PriceEntity, DateTime, bool> Action => ProductLogic.Add;
}