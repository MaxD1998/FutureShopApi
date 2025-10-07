using Shared.Shared.Interfaces;
using Shop.Core.Interfaces;
using Shop.Core.Logics.PromotionLogics.Models;
using Shop.Infrastructure.Models.Promotions;
using System.Text.Json;

namespace Shop.Core.Logics.PromotionLogics;

public class SetPercentPromotionForProductsLogic<T>() : ILogic<SetPromotionForProductsRequestModel<T>, List<T>> where T : IProductPrice
{
    public Task<List<T>> ExecuteAsync(SetPromotionForProductsRequestModel<T> request, CancellationToken cancellationToken)
    {
        var productIds = request.Promotion.PromotionProducts.Select(p => p.ProductId).ToList();
        var products = request.Products.ToList();
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var percentValue = request.Promotion.Value.Deserialize<PercentPromotionValue>(options);

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