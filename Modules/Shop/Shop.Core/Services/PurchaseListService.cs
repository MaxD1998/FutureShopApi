﻿using MediatR;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Core.Errors;
using Shop.Core.Cqrs.Basket.Queries;
using Shop.Core.Cqrs.PurchaseList.Commands;
using Shop.Core.Cqrs.PurchaseList.Queries;
using Shop.Core.Dtos.PurchaseList;
using Shop.Domain.Entities;
using System.Net;

namespace Shop.Core.Services;

public interface IPurchaseListService
{
    public Task<ResultDto<PurchaseListDto>> ImportBasketAsync(ImportBasketToPurchaseListDto dto, CancellationToken cancellationToken = default);
}

public class PurchaseListService(IMediator mediator) : BaseService, IPurchaseListService
{
    private readonly IMediator _mediator = mediator;

    public async Task<ResultDto<PurchaseListDto>> ImportBasketAsync(ImportBasketToPurchaseListDto dto, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var basket = await _mediator.Send(new GetBasketEntityByIdQuery(dto.BasketId), cancellationToken);

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

        var entityResult = await _mediator.Send(new CreatePurchaseListEntityCommand(purchaseList), cancellationToken);

        if (!entityResult.IsSuccess)
            return Error<PurchaseListDto>(entityResult.HttpCode, entityResult.ErrorMessage);

        return await _mediator.Send(new GetPurchaseListDtoByIdQuery(entityResult.Result.Id), cancellationToken);
    }
}