﻿using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Core.Errors;
using Shared.Core.Services;
using Shop.Core.Dtos.PurchaseList;
using Shop.Infrastructure.Entities;
using Shop.Infrastructure.Repositories;
using System.Net;

namespace Shop.Core.Services;

public interface IPurchaseListService
{
    Task<ResultDto<PurchaseListResponseFormDto>> CreateAsync(PurchaseListRequestFormDto dto, CancellationToken cancellationToken);

    Task<ResultDto> DeleteByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<PurchaseListDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<List<PurchaseListDto>>> GetListByAuthorizedUserAsync(CancellationToken cancellationToken);

    Task<ResultDto<PurchaseListDto>> ImportBasketAsync(ImportBasketToPurchaseListDto dto, CancellationToken cancellationToken);

    Task<ResultDto<PurchaseListResponseFormDto>> UpdateAsync(Guid id, PurchaseListRequestFormDto dto, CancellationToken cancellationToken);
}

internal class PurchaseListService(IBasketRepository basketRepository, ICurrentUserService currentUserService, IPurchaseListRepository purchaseListRepository) : BaseService, IPurchaseListService
{
    private readonly IBasketRepository _basketRepository = basketRepository;
    private readonly ICurrentUserService _currentUserService = currentUserService;
    private readonly IPurchaseListRepository _purchaseListRepository = purchaseListRepository;

    public async Task<ResultDto<PurchaseListResponseFormDto>> CreateAsync(PurchaseListRequestFormDto dto, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.GetUserId();
        var entity = await _purchaseListRepository.CreateAsync(dto.ToEntity(userId), cancellationToken);
        var result = await _purchaseListRepository.GetByIdAsync(entity.Id, PurchaseListResponseFormDto.Map(), cancellationToken);

        return Success(result);
    }

    public async Task<ResultDto> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _purchaseListRepository.DeleteByIdAsync(id, cancellationToken);

        return Success();
    }

    public async Task<ResultDto<PurchaseListDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _purchaseListRepository.GetByIdAsync(id, PurchaseListDto.Map(), cancellationToken);

        return Success(result);
    }

    public async Task<ResultDto<List<PurchaseListDto>>> GetListByAuthorizedUserAsync(CancellationToken cancellationToken)
    {
        var userId = _currentUserService.GetUserId();

        if (userId == null)
            return Success<List<PurchaseListDto>>(null);

        var results = await _purchaseListRepository.GetByUserIdAsync(userId.Value, PurchaseListDto.Map(), cancellationToken);

        return Success(results);
    }

    public async Task<ResultDto<PurchaseListDto>> ImportBasketAsync(ImportBasketToPurchaseListDto dto, CancellationToken cancellationToken)
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
        var result = await _purchaseListRepository.GetByIdAsync(entity.Id, PurchaseListDto.Map(), cancellationToken);

        return Success(result);
    }

    public async Task<ResultDto<PurchaseListResponseFormDto>> UpdateAsync(Guid id, PurchaseListRequestFormDto dto, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.GetUserId();
        var entity = await _purchaseListRepository.UpdateAsync(id, dto.ToEntity(userId), cancellationToken);
        var result = await _purchaseListRepository.GetByIdAsync(entity.Id, PurchaseListResponseFormDto.Map(), cancellationToken);

        return Success(result);
    }
}