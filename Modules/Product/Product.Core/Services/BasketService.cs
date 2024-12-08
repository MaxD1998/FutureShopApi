using MediatR;
using Product.Core.Cqrs.Basket.Commands;
using Product.Core.Cqrs.Basket.Queries;
using Product.Core.Cqrs.PurchaseList.Queries;
using Product.Core.Dtos.Basket;
using Shared.Core.Errors;
using Shared.Core.Exceptions;

namespace Product.Core.Services;

public interface IBasketSerivce
{
    public Task<BasketDto> ImportPurchaseListAsync(ImportPurchaseListToBasketDto dto, CancellationToken cancellationToken = default);
}

public class BasketService(IMediator mediator) : IBasketSerivce
{
    private readonly IMediator _mediator = mediator;

    public async Task<BasketDto> ImportPurchaseListAsync(ImportPurchaseListToBasketDto dto, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var basketTask = _mediator.Send(new GetBasketEntityByIdQuery(dto.BasketId), cancellationToken);
        var purchaseListTask = _mediator.Send(new GetPurchaseListEntityByIdQuery(dto.PurchaseListId), cancellationToken);

        await Task.WhenAll(basketTask, purchaseListTask);

        var basket = basketTask.Result;
        var purchaseList = purchaseListTask.Result;

        if (basket is null || purchaseList is null)
            throw new NotFoundException(CommonExceptionMessage.C007RecordWasNotFound);

        foreach (var purchaseListItem in purchaseList.PurchaseListItems)
        {
            basket.BasketItems.Add(new()
            {
                ProductId = purchaseListItem.ProductId,
            });
        }

        var entity = await _mediator.Send(new UpdateBasketEntityCommand(basket.Id, basket), cancellationToken);

        return await _mediator.Send(new GetBasketDtoByIdQuery(entity.Id, dto.PurchaseListId), cancellationToken);
    }
}