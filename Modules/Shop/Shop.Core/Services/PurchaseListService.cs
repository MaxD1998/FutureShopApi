using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Core.Errors;
using Shop.Core.Cqrs.PurchaseList.Commands;
using Shop.Core.Cqrs.PurchaseList.Queries;
using Shop.Core.Dtos.PurchaseList;
using Shop.Domain.Entities;
using Shop.Infrastructure.Repositories;
using System.Net;

namespace Shop.Core.Services;

public interface IPurchaseListService
{
    public Task<ResultDto<PurchaseListDto>> ImportBasketAsync(ImportBasketToPurchaseListDto dto, CancellationToken cancellationToken = default);
}

public class PurchaseListService(IBasketRepository basketRepository, IPurchaseListRepository purchaseListRepository) : BaseService, IPurchaseListService
{
    private readonly IBasketRepository _basketRepository = basketRepository;
    private readonly IPurchaseListRepository _purchaseListRepository = purchaseListRepository;

    public async Task<ResultDto<PurchaseListDto>> ImportBasketAsync(ImportBasketToPurchaseListDto dto, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var basket = await _basketRepository.GetByIdAsync(dto.BasketId, cancellationToken);

        if (basket is null)
            return Error<PurchaseListDto>(HttpStatusCode.NotFound, CommonExceptionMessage.C007RecordWasNotFound);

        var purchaseList = new PurchaseListEntity
        {
            Name = dto.Name,
        };

        foreach (var basketItem in basket.BasketItems)
        {
            purchaseList.PurchaseListItems.Add(new()
            {
                ProductId = basketItem.ProductId,
            });
        }

        var entity = await _purchaseListRepository.CreateAsync(purchaseList, cancellationToken);

        return await _mediator.Send(new GetPurchaseListDtoByIdQuery(entityResult.Result.Id), cancellationToken);
    }
}