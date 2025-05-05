using Microsoft.AspNetCore.Http;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Core.Errors;
using Shared.Core.Extensions;
using Shop.Core.Dtos.PurchaseList;
using Shop.Domain.Entities;
using Shop.Infrastructure.Repositories;
using System.Net;

namespace Shop.Core.Services;

public interface IPurchaseListService
{
    Task<ResultDto<PurchaseListFormDto>> CreateAsync(PurchaseListFormDto dto, CancellationToken cancellationToken);

    Task<ResultDto> DeleteByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<PurchaseListDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<List<PurchaseListDto>>> GetListByAuthorizedUserAsync(CancellationToken cancellationToken);

    Task<ResultDto<PurchaseListDto>> ImportBasketAsync(ImportBasketToPurchaseListDto dto, CancellationToken cancellationToken);

    Task<ResultDto<PurchaseListFormDto>> UpdateAsync(Guid id, PurchaseListFormDto dto, CancellationToken cancellationToken);
}

public class PurchaseListService(IBasketRepository basketRepository, IHttpContextAccessor httpContextAccessor, IPurchaseListRepository purchaseListRepository) : BaseService, IPurchaseListService
{
    private readonly IBasketRepository _basketRepository = basketRepository;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IPurchaseListRepository _purchaseListRepository = purchaseListRepository;

    public async Task<ResultDto<PurchaseListFormDto>> CreateAsync(PurchaseListFormDto dto, CancellationToken cancellationToken)
    {
        var entity = await _purchaseListRepository.CreateAsync(dto.ToEntity(), cancellationToken);
        var result = await _purchaseListRepository.GetByIdAsync(entity.Id, PurchaseListFormDto.Map(), cancellationToken);

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
        var userId = _httpContextAccessor.GetUserId();

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

    public async Task<ResultDto<PurchaseListFormDto>> UpdateAsync(Guid id, PurchaseListFormDto dto, CancellationToken cancellationToken)
    {
        var entity = await _purchaseListRepository.UpdateAsync(id, dto.ToEntity(), cancellationToken);
        var result = await _purchaseListRepository.GetByIdAsync(entity.Id, PurchaseListFormDto.Map(), cancellationToken);

        return Success(result);
    }
}