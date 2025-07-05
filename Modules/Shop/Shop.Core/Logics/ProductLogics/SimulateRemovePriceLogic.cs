using Shared.Core.Dtos;
using Shop.Core.Dtos;
using Shop.Core.Dtos.Price;
using Shop.Core.Interfaces;
using Shop.Domain.Logics;

namespace Shop.Core.Logics.ProductLogics;

internal class SimulateRemovePriceLogic(ILogic<Guid?, bool> getProductWasActiveLogic) : ILogic<SimulateRemovePriceRequestDto, ResultDto<List<SimulatePriceFormDto>>>
{
    private readonly ILogic<Guid?, bool> _getProductWasActiveLogic = getProductWasActiveLogic;

    public async Task<ResultDto<List<SimulatePriceFormDto>>> ExecuteAsync(SimulateRemovePriceRequestDto request, CancellationToken cancellationToken)
    {
        var wasActive = await _getProductWasActiveLogic.ExecuteAsync(request.ProductId, cancellationToken);

        foreach (var element in request.OldCollection.Where(x => x.IsNew))
        {
            element.Id = Guid.NewGuid();

            var newElement = request.NewCollection.FirstOrDefault(x => x.FakeId == element.FakeId);
            if (newElement != null)
                newElement.Id = element.Id;
        }

        var oldCollection = request.OldCollection.Select(x => x.ToEntity()).ToList();
        var newCollection = request.NewCollection.Select(x => x.ToEntity()).ToList();

        ProductLogic.Remove(oldCollection, newCollection, DateTime.UtcNow, wasActive);

        var results = oldCollection.Select(SimulatePriceFormDto.Map()).ToList();

        foreach (var result in results)
        {
            if (request.OldCollection.Any(x => x.Id == result.Id && x.IsNew))
            {
                result.Id = null;
                result.IsNew = true;
            }
        }

        return ResultDto.Success(results);
    }
}