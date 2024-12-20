using MediatR;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Core.Errors;
using Shop.Core.Cqrs.Basket.Commands;
using Shop.Core.Cqrs.Basket.Queries;
using Shop.Core.Cqrs.PurchaseList.Queries;
using Shop.Core.Dtos.Basket;
using System.Net;

namespace Shop.Core.Services;

public interface IBasketSerivce
{
    public Task<ResultDto<BasketDto>> ImportPurchaseListAsync(ImportPurchaseListToBasketDto dto, CancellationToken cancellationToken = default);
}

public class BasketService(IMediator mediator) : BaseService, IBasketSerivce
{
    private readonly IMediator _mediator = mediator;

    public async Task<ResultDto<BasketDto>> ImportPurchaseListAsync(ImportPurchaseListToBasketDto dto, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var basketTask = _mediator.Send(new GetBasketEntityByIdQuery(dto.BasketId), cancellationToken);
        var purchaseListTask = _mediator.Send(new GetPurchaseListEntityByIdQuery(dto.PurchaseListId), cancellationToken);

        await Task.WhenAll(basketTask, purchaseListTask);

        var basket = basketTask.Result;
        var purchaseList = purchaseListTask.Result;

        if (basket is null || purchaseList is null)
            return Error<BasketDto>(HttpStatusCode.NotFound, CommonExceptionMessage.C007RecordWasNotFound);

        foreach (var purchaseListItem in purchaseList.PurchaseListItems)
        {
            basket.BasketItems.Add(new()
            {
                ProductId = purchaseListItem.ProductId,
            });
        }

        var entityResult = await _mediator.Send(new UpdateBasketEntityCommand(basket.Id, basket), cancellationToken);

        if (!entityResult.IsSuccess)
            return Error<BasketDto>(entityResult.HttpCode, entityResult.ErrorMessage);

        return await _mediator.Send(new GetBasketDtoByIdQuery(entityResult.Result.Id, dto.PurchaseListId), cancellationToken);
    }
}