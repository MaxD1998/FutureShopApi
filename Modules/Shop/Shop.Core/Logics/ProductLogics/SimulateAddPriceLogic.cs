using Shop.Core.Interfaces;
using Shop.Infrastructure.Entities;
using Shop.Infrastructure.Helpers;

namespace Shop.Core.Logics.ProductLogics;

internal class SimulateAddPriceLogic(ILogic<GetProductWasActiveModel, bool> getProductWasActiveLogic) : BaseSimulatePriceActionLogic(getProductWasActiveLogic)
{
    protected override Action<ICollection<PriceEntity>, PriceEntity, DateTime, bool> Action => PriceHelper.Add;
}