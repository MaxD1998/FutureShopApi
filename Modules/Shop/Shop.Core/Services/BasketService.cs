using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Core.Errors;
using Shared.Core.Services;
using Shop.Core.Dtos.Basket;
using Shop.Core.Factories;
using Shop.Core.Logics.Baskets;
using Shop.Infrastructure.Repositories;
using System.Net;

namespace Shop.Core.Services;

public interface IBasketSerivce
{
    Task<ResultDto<BasketFormResponseDto>> CreateAsync(BasketFormRequestDto dto, CancellationToken cancellationToken);

    Task<ResultDto<BasketDto>> GetByAuthorizedUserAsync(CancellationToken cancellationToken);

    Task<ResultDto<BasketDto>> GetByIdAsync(Guid id, Guid? favouriteId, CancellationToken cancellationToken);

    Task<ResultDto<BasketDto>> ImportPurchaseListAsync(ImportPurchaseListToBasketDto dto, CancellationToken cancellationToken);

    Task<ResultDto<BasketFormResponseDto>> UpdateAsync(Guid id, BasketFormRequestDto dto, CancellationToken cancellationToken);
}

public class BasketService(IBasketRepository basketRepository, ICurrentUserService currentUserService, IProductRepository productRepository, IPurchaseListRepository purchaseListRepository) : BaseService, IBasketSerivce
{
    private readonly IBasketRepository _basketRepository = basketRepository;
    private readonly ICurrentUserService _currentUserService = currentUserService;
    private readonly ILogicFactory _logicFactory = new LogicFactory();
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IPurchaseListRepository _purchaseListRepository = purchaseListRepository;

    public async Task<ResultDto<BasketFormResponseDto>> CreateAsync(BasketFormRequestDto dto, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.GetUserId();
        var entity = await _basketRepository.CreateAsync(dto.ToEntity(userId), cancellationToken);
        var result = await _basketRepository.GetByIdAsync(entity.Id, BasketFormResponseDto.Map(), cancellationToken);

        return Success(result);
    }

    public async Task<ResultDto<BasketDto>> GetByAuthorizedUserAsync(CancellationToken cancellationToken)
    {
        var userId = _currentUserService.GetUserId();

        if (!userId.HasValue)
            return Success<BasketDto>(null);

        var requestModel = new GetBasketDtoByUserIdRequestModel(userId.Value);

        return await _logicFactory.ExecuteAsync(requestModel, f => f.CreateGetBasketDtoByUserIdLogic(_basketRepository, _productRepository), cancellationToken);
    }

    public async Task<ResultDto<BasketDto>> GetByIdAsync(Guid id, Guid? favouriteId, CancellationToken cancellationToken)
    {
        var requestModel = new GetBasketDtoByIdRequestModel(id, favouriteId);

        return await _logicFactory.ExecuteAsync(requestModel, f => f.CreateGetBasketDtoByIdLogic(_basketRepository, _productRepository), cancellationToken);
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
            basket.AddBasketItem(new(purchaseListItem.ProductId, 1));
        }

        var entity = await _basketRepository.UpdateAsync(basket.Id, basket, cancellationToken);
        var requestModel = new GetBasketDtoByIdRequestModel(entity.Id, dto.PurchaseListId);

        return await _logicFactory.ExecuteAsync(requestModel, f => f.CreateGetBasketDtoByIdLogic(_basketRepository, _productRepository), cancellationToken);
    }

    public async Task<ResultDto<BasketFormResponseDto>> UpdateAsync(Guid id, BasketFormRequestDto dto, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.GetUserId();
        var entity = await _basketRepository.UpdateAsync(id, dto.ToEntity(userId), cancellationToken);
        var result = await _basketRepository.GetByIdAsync(entity.Id, BasketFormResponseDto.Map(), cancellationToken);

        return Success(result);
    }
}