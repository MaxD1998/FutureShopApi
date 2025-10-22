using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Core.Errors;
using Shared.Core.Services;
using Shared.Infrastructure.Constants;
using Shared.Shared.Extensions;
using Shop.Core.Dtos.Basket;
using Shop.Core.Dtos.Basket.BasketItem;
using Shop.Core.Factories;
using Shop.Core.Logics.PromotionLogics;
using Shop.Infrastructure.Repositories;
using System.Net;

namespace Shop.Core.Services;

public interface IBasketSerivce
{
    Task<ResultDto<BasketResponseFormDto>> CreateAsync(BasketRequestFormDto dto, CancellationToken cancellationToken);

    Task<ResultDto<BasketDto>> GetByAuthorizedUserAsync(CancellationToken cancellationToken);

    Task<ResultDto<BasketDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<BasketDto>> ImportPurchaseListAsync(ImportPurchaseListToBasketDto dto, CancellationToken cancellationToken);

    Task<ResultDto<BasketResponseFormDto>> UpdateAsync(Guid id, BasketRequestFormDto dto, CancellationToken cancellationToken);
}

internal class BasketService(
    IBasketRepository basketRepository,
    ICurrentUserService currentUserService,
    IHeaderService headerService,
    ILogicFactory logicFactory,
    IPromotionRepository promotionRepository,
    IPurchaseListRepository purchaseListRepository) : BaseService, IBasketSerivce
{
    private readonly IBasketRepository _basketRepository = basketRepository;
    private readonly ICurrentUserService _currentUserService = currentUserService;
    private readonly IHeaderService _headerService = headerService;
    private readonly ILogicFactory _logicFactory = logicFactory;
    private readonly IPromotionRepository _promotionRepository = promotionRepository;
    private readonly IPurchaseListRepository _purchaseListRepository = purchaseListRepository;

    public async Task<ResultDto<BasketResponseFormDto>> CreateAsync(BasketRequestFormDto dto, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.GetUserId();
        var entity = await _basketRepository.CreateAsync(dto.ToEntity(userId), cancellationToken);
        var result = await _basketRepository.GetByIdAsync(entity.Id, BasketResponseFormDto.Map(), cancellationToken);

        return Success(result);
    }

    public async Task<ResultDto<BasketDto>> GetByAuthorizedUserAsync(CancellationToken cancellationToken)
    {
        var userId = _currentUserService.GetUserId();

        if (!userId.HasValue)
            return Success<BasketDto>(null);

        var result = await _basketRepository.GetByUserIdAsync(userId.Value, BasketDto.Map(x => x.PurchaseList.UserId == userId), cancellationToken);

        if (result == null)
            return Success(result);

        var codes = _headerService.GetHeader(HeaderNameConst.Codes).ToListString();
        var productList = result.BasketItems.Select(x => x.Product).ToList();
        var promotionRequest = new SetPromotionForProductsRequestModel<BasketItemProductDto>(codes, productList);

        await _logicFactory.ExecuteAsync(promotionRequest, f => f.SetPromotionForProductsLogic<BasketItemProductDto>(), cancellationToken);

        return Success(result);
    }

    public async Task<ResultDto<BasketDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var favouriteId = _headerService.GetHeader(HeaderNameConst.FavouriteId).ToNullableGuid();
        var result = await _basketRepository.GetByIdAsync(id, BasketDto.Map(x => x.PurchaseListId == favouriteId), cancellationToken);

        return Success(result);
    }

    public async Task<ResultDto<BasketDto>> ImportPurchaseListAsync(ImportPurchaseListToBasketDto dto, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var basket = await _basketRepository.GetByIdAsync(dto.BasketId, cancellationToken);
        var purchaseList = await _purchaseListRepository.GetByIdAsync(dto.PurchaseListId, cancellationToken);

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

    public async Task<ResultDto<BasketResponseFormDto>> UpdateAsync(Guid id, BasketRequestFormDto dto, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.GetUserId();
        var entity = await _basketRepository.UpdateAsync(id, dto.ToEntity(userId), cancellationToken);
        var result = await _basketRepository.GetByIdAsync(entity.Id, BasketResponseFormDto.Map(), cancellationToken);

        return Success(result);
    }
}