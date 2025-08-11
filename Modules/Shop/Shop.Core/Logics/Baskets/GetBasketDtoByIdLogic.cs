using Shared.Core.Dtos;
using Shop.Core.Dtos.Basket;
using Shop.Core.Interfaces;
using Shop.Infrastructure.Repositories;

namespace Shop.Core.Logics.Baskets;

internal record GetBasketDtoByIdRequestModel(Guid Id, Guid? PurchaseListId);

internal class GetBasketDtoByIdLogic(
    IBasketRepository basketRepository,
    IProductRepository productRepository) : BaseGetBasketDtoLogic(basketRepository, productRepository), ILogic<GetBasketDtoByIdRequestModel, ResultDto<BasketDto>>
{
    public async Task<ResultDto<BasketDto>> ExecuteAsync(GetBasketDtoByIdRequestModel request, CancellationToken cancellationToken)
    {
        var id = request.Id;
        var purchaseListId = request.PurchaseListId;
        var basket = await _basketRepository.GetByIdAsync(id, BasketDto.Map(), cancellationToken);
        var result = await ToBasketDtoAsync(basket, x => x.PurchaseListId == purchaseListId, cancellationToken);

        return ResultDto.Success(result);
    }
}