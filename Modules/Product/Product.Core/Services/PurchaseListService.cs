using MediatR;
using Product.Core.Cqrs.Basket.Queries;
using Product.Core.Cqrs.PurchaseList.Commands;
using Product.Core.Cqrs.PurchaseList.Queries;
using Product.Core.Dtos.PurchaseList;
using Product.Domain.Entities;
using Shared.Core.Errors;
using Shared.Core.Exceptions;

namespace Product.Core.Services;

public interface IPurchaseListService
{
    public Task<PurchaseListDto> ImportBasketAsync(ImportBasketToPurchaseListDto dto, CancellationToken cancellationToken = default);
}

public class PurchaseListService(IMediator mediator) : IPurchaseListService
{
    private readonly IMediator _mediator = mediator;

    public async Task<PurchaseListDto> ImportBasketAsync(ImportBasketToPurchaseListDto dto, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var basket = await _mediator.Send(new GetBasketEntityByIdQuery(dto.BasketId), cancellationToken);

        if (basket is null)
            throw new NotFoundException(CommonExceptionMessage.C007RecordWasNotFound);

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

        var entity = await _mediator.Send(new CreatePurchaseListEntityCommand(purchaseList), cancellationToken);

        return await _mediator.Send(new GetPurchaseListDtoByIdQuery(entity.Id), cancellationToken);
    }
}