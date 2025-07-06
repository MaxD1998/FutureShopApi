using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Core.Errors;
using Shared.Core.Services;
using Shop.Core.Dtos.Basket;
using Shop.Infrastructure.Repositories;
using System.Net;

namespace Shop.Core.Services;

public interface IBasketSerivce
{
    Task<ResultDto<BasketFormDto>> CreateAsync(BasketFormDto dto, CancellationToken cancellationToken);

    Task<ResultDto<BasketDto>> GetByAuthorizedUserAsync(CancellationToken cancellationToken);

    Task<ResultDto<BasketDto>> GetByIdAsync(Guid id, Guid? favouriteId, CancellationToken cancellationToken);

    Task<ResultDto<BasketDto>> ImportPurchaseListAsync(ImportPurchaseListToBasketDto dto, CancellationToken cancellationToken);

    Task<ResultDto<BasketFormDto>> UpdateAsync(Guid id, BasketFormDto dto, CancellationToken cancellationToken);
}

public class BasketService(IBasketRepository basketRepository, ICurrentUserService currentUserService, IPurchaseListRepository purchaseListRepository) : BaseService, IBasketSerivce
{
    private readonly IBasketRepository _basketRepository = basketRepository;
    private readonly ICurrentUserService _currentUserService = currentUserService;
    private readonly IPurchaseListRepository _purchaseListRepository = purchaseListRepository;

    public async Task<ResultDto<BasketFormDto>> CreateAsync(BasketFormDto dto, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.GetUserId();
        var entity = await _basketRepository.CreateAsync(dto.ToEntity(userId), cancellationToken);
        var result = await _basketRepository.GetByIdAsync(entity.Id, BasketFormDto.Map(), cancellationToken);

        return Success(result);
    }

    public async Task<ResultDto<BasketDto>> GetByAuthorizedUserAsync(CancellationToken cancellationToken)
    {
        var userId = _currentUserService.GetUserId();

        if (!userId.HasValue)
            return Success<BasketDto>(null);

        var result = await _basketRepository.GetByUserIdAsync(userId.Value, BasketDto.Map(x => x.PurchaseList.UserId == userId), cancellationToken);

        return Success(result);
    }

    public async Task<ResultDto<BasketDto>> GetByIdAsync(Guid id, Guid? favouriteId, CancellationToken cancellationToken)
    {
        var result = await _basketRepository.GetByIdAsync(id, BasketDto.Map(x => x.PurchaseListId == favouriteId), cancellationToken);

        return Success(result);
    }

    public async Task<ResultDto<BasketDto>> ImportPurchaseListAsync(ImportPurchaseListToBasketDto dto, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var basketTask = _basketRepository.GetByIdAsync(dto.BasketId, cancellationToken);
        var purchaseListTask = _purchaseListRepository.GetByIdAsync(dto.PurchaseListId, cancellationToken);

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

        var entity = await _basketRepository.UpdateAsync(basket.Id, basket, cancellationToken);
        var result = await _basketRepository.GetByIdAsync(entity.Id, BasketDto.Map(x => x.PurchaseListId == dto.PurchaseListId), cancellationToken);

        return Success(result);
    }

    public async Task<ResultDto<BasketFormDto>> UpdateAsync(Guid id, BasketFormDto dto, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.GetUserId();
        var entity = await _basketRepository.UpdateAsync(id, dto.ToEntity(userId), cancellationToken);
        var result = await _basketRepository.GetByIdAsync(entity.Id, BasketFormDto.Map(), cancellationToken);

        return Success(result);
    }
}