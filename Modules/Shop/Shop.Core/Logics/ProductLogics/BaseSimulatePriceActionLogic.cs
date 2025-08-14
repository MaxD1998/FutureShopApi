using Shared.Core.Dtos;
using Shared.Shared.Interfaces;
using Shop.Core.Dtos;
using Shop.Core.Dtos.Price;
using Shop.Infrastructure.Entities;
using Shop.Infrastructure.Repositories;

namespace Shop.Core.Logics.ProductLogics;

internal abstract class BaseSimulatePriceActionLogic(IProductRepository productRepository) : BasePriceLogic(productRepository), ILogic<SimulatePriceRequestDto, ResultDto<List<SimulatePriceFormDto>>>
{
    protected abstract Action<ICollection<PriceEntity>, PriceEntity, DateTime, bool> Action { get; }

    public async Task<ResultDto<List<SimulatePriceFormDto>>> ExecuteAsync(SimulatePriceRequestDto request, CancellationToken cancellationToken)
    {
        var wasActive = await GetProductWasActiveByIdAsync(request.ProductId, cancellationToken);

        if (request.Element.FakeId == 0)
            request.Element.Id = Guid.NewGuid();

        foreach (var element in request.Collection.Where(x => x.IsNew))
        {
            element.Id = Guid.NewGuid();

            if (request.Element.FakeId == element.FakeId)
                request.Element.Id = element.Id;
        }

        var collection = request.Collection.Select(x => x.ToEntity()).ToList();
        var entity = request.Element.ToEntity();

        Action(collection, entity, DateTime.UtcNow, wasActive);

        var results = collection.Select(SimulatePriceFormDto.Map()).ToList();

        foreach (var result in results)
        {
            if (request.Collection.Any(x => x.Id == result.Id && x.IsNew) || (request.Element.Id == result.Id && request.Element.IsNew))
            {
                result.Id = null;
                result.IsNew = true;
            }
        }

        return ResultDto.Success(results);
    }
}