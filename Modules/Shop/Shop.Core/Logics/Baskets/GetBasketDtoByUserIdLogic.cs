using Shared.Core.Dtos;
using Shop.Core.Dtos.Basket;
using Shop.Core.Interfaces;
using Shop.Infrastructure.Repositories;

namespace Shop.Core.Logics.Baskets;

internal record GetBasketDtoByUserIdRequestModel(Guid UserId);

internal class GetBasketDtoByUserIdLogic(
    IBasketRepository basketRepository,
    IProductRepository productRepository) : BaseGetBasketDtoLogic(basketRepository, productRepository), ILogic<GetBasketDtoByUserIdRequestModel, ResultDto<BasketDto>>
{
    public async Task<ResultDto<BasketDto>> ExecuteAsync(GetBasketDtoByUserIdRequestModel request, CancellationToken cancellationToken)
    {
        var userId = request.UserId;
        var basket = await _basketRepository.GetByUserIdAsync(userId, BasketDto.Map(), cancellationToken);
        var result = await ToBasketDtoAsync(basket, x => x.PurchaseList.UserId == userId, cancellationToken);

        return ResultDto.Success(result);
    }
}