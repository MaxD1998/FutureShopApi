using Shop.Core.Dtos.Basket;
using Shop.Core.Dtos.Product;
using Shop.Domain.Aggregates.PurchaseLists.Entities;
using Shop.Infrastructure.Repositories;
using System.Linq.Expressions;

namespace Shop.Core.Logics.Baskets;

internal abstract class BaseGetBasketDtoLogic(IBasketRepository basketRepository, IProductRepository productRepository)
{
    protected readonly IBasketRepository _basketRepository = basketRepository;
    protected readonly IProductRepository _productRepository = productRepository;

    protected async Task<BasketDto> ToBasketDtoAsync(BasketDto basket, Expression<Func<PurchaseListItemEntity, bool>> isInPurchaseListPredicate, CancellationToken cancellationToken)
    {
        if (basket == null)
            return null;

        var productIds = basket.BasketItems.Select(x => x.ProductId).ToList();
        var products = await _productRepository.GetListAsync(x => productIds.Contains(x.Id), ProductBasketItemDto.Map(isInPurchaseListPredicate), cancellationToken);

        foreach (var item in basket.BasketItems)
        {
            var product = products.Find(x => x.Id == item.ProductId);
            item.Product = product;
        }

        return basket;
    }
}