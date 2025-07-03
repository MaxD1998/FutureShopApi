using Shared.Core.Dtos;
using Shop.Core.Dtos;
using Shop.Core.Dtos.Price;
using Shop.Core.Interfaces;
using Shop.Domain.Entities;

namespace Shop.Core.Logics.ProductLogics;

internal abstract class BaseSimulatePriceActionLogic(ILogic<Guid?, bool> getProductWasActiveLogic) : ILogic<SimulatePriceRequest, ResultDto<List<PriceFormDto>>>
{
    private readonly ILogic<Guid?, bool> _getProductWasActiveLogic = getProductWasActiveLogic;

    protected abstract Action<ICollection<PriceEntity>, PriceEntity, DateTime, bool> Action { get; }

    public async Task<ResultDto<List<PriceFormDto>>> ExecuteAsync(SimulatePriceRequest request, CancellationToken cancellationToken)
    {
        var wasActive = await _getProductWasActiveLogic.ExecuteAsync(request.ProductId, cancellationToken);

        var collection = request.Collection.Select(x => x.ToEntity()).ToList();
        var entity = request.Element.ToEntity();

        Action(collection, entity, DateTime.UtcNow, wasActive);

        var results = collection.AsQueryable().Select(PriceFormDto.Map()).ToList();

        return ResultDto.Success(results);
    }
}