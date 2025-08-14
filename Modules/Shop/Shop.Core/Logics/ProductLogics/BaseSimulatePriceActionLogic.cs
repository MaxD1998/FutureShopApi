using Shared.Core.Dtos;
using Shop.Core.Dtos;
using Shop.Core.Dtos.Price;
using Shop.Core.Interfaces;
using Shop.Infrastructure.Entities;

namespace Shop.Core.Logics.ProductLogics;

internal abstract class BaseSimulatePriceActionLogic(ILogic<GetProductWasActiveModel, bool> getProductWasActiveLogic) : ILogic<SimulatePriceRequestDto, ResultDto<List<SimulatePriceFormDto>>>
{
    private readonly ILogic<GetProductWasActiveModel, bool> _getProductWasActiveLogic = getProductWasActiveLogic;

    protected abstract Action<ICollection<PriceEntity>, PriceEntity, DateTime, bool> Action { get; }

    public async Task<ResultDto<List<SimulatePriceFormDto>>> ExecuteAsync(SimulatePriceRequestDto request, CancellationToken cancellationToken)
    {
        var model = new GetProductWasActiveModel(request.ProductId);
        var wasActive = await _getProductWasActiveLogic.ExecuteAsync(model, cancellationToken);

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