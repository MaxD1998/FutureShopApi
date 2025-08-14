using Shared.Core.Dtos;
using Shared.Shared.Interfaces;
using Shop.Core.Dtos;
using Shop.Core.Dtos.Price;
using Shop.Infrastructure.DomainLogics;
using Shop.Infrastructure.Repositories;

namespace Shop.Core.Logics.ProductLogics;

internal class SimulateRemovePriceLogic(IProductRepository productRepository) : BasePriceLogic(productRepository), ILogic<SimulateRemovePriceRequestDto, ResultDto<List<SimulatePriceFormDto>>>
{
    public async Task<ResultDto<List<SimulatePriceFormDto>>> ExecuteAsync(SimulateRemovePriceRequestDto request, CancellationToken cancellationToken)
    {
        var wasActive = await GetProductWasActiveByIdAsync(request.ProductId, cancellationToken);

        foreach (var element in request.OldCollection.Where(x => x.IsNew))
        {
            element.Id = Guid.NewGuid();

            var newElement = request.NewCollection.FirstOrDefault(x => x.FakeId == element.FakeId);
            if (newElement != null)
                newElement.Id = element.Id;
        }

        var oldCollection = request.OldCollection.Select(x => x.ToEntity()).ToList();
        var newCollection = request.NewCollection.Select(x => x.ToEntity()).ToList();

        PriceDomainLogic.Remove(oldCollection, newCollection, DateTime.UtcNow, wasActive);

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