using Shared.Shared.Interfaces;
using Shop.Core.Interfaces;
using Shop.Infrastructure.Persistence.Entities.Promotions;
using Shop.Infrastructure.Persistence.Enums;
using Shop.Infrastructure.Persistence.Models.Promotions;
using Shop.Infrastructure.Persistence.Repositories;
using System.Text.Json;

namespace Shop.Core.Logics.PromotionLogics;

public record SetPromotionForProductsRequestModel<T>(List<string> Codes, List<T> Products) where T : IProductPrice;

internal class SetPromotionForProductsLogic<T>(IPromotionRepository promotionRepository) : ILogic<SetPromotionForProductsRequestModel<T>, List<T>> where T : IProductPrice
{
    private readonly IPromotionRepository _promotionRepository = promotionRepository;

    public async Task<List<T>> ExecuteAsync(SetPromotionForProductsRequestModel<T> request, CancellationToken cancellationToken)
    {
        var promotions = await _promotionRepository.GetActivePromotionsAsync(request.Codes, cancellationToken);

        if (promotions != null && promotions.Count > 0)
        {
            var products = request.Products.ToList();
            foreach (var promotion in promotions)
            {
                products = promotion.Type switch
                {
                    PromotionType.Percent => await SetPercentPromotionAsync(promotion, products, cancellationToken),
                    _ => request.Products
                };
            }

            return products;
        }

        return request.Products;
    }

    public Task<List<T>> SetPercentPromotionAsync(PromotionEntity promotion, List<T> products, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var productIds = promotion.PromotionProducts.Select(p => p.ProductId).ToList();
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var percentValue = promotion.Value.Deserialize<PercentPromotionValue>(options);

        foreach (var productId in productIds)
        {
            var product = products.FirstOrDefault(x => x.Id == productId);

            if (product == null)
                continue;

            var price = product.OriginalPrice * (1 - percentValue.Percent / 100);

            if (product.Price > price)
                product.Price = price;
        }

        return Task.FromResult(products);
    }
}