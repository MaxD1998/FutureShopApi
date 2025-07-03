using Shared.Core.Dtos;
using Shop.Core.Dtos;
using Shop.Core.Dtos.Price;
using Shop.Core.Interfaces;
using Shop.Domain.Logics;

namespace Shop.Core.Logics.ProductLogics;

internal class SimulateRemovePriceLogic(ILogic<Guid?, bool> getProductWasActiveLogic) : ILogic<SimulateRemovePriceRequest, ResultDto<List<PriceFormDto>>>
{
    private readonly ILogic<Guid?, bool> _getProductWasActiveLogic = getProductWasActiveLogic;

    public async Task<ResultDto<List<PriceFormDto>>> ExecuteAsync(SimulateRemovePriceRequest request, CancellationToken cancellationToken)
    {
        var wasActive = await _getProductWasActiveLogic.ExecuteAsync(request.ProductId, cancellationToken);

        var oldCollection = request.OldCollection.Select(x => x.ToEntity()).ToList();
        var newCollection = request.NewCollection.Select(x => x.ToEntity()).ToList();

        ProductLogic.Remove(oldCollection, newCollection, DateTime.UtcNow, wasActive);

        var results = oldCollection.AsQueryable().Select(PriceFormDto.Map()).ToList();

        return ResultDto.Success(results);
    }
}