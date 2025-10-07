using Shop.Core.Interfaces;
using Shop.Infrastructure.Entities.Promotions;

namespace Shop.Core.Logics.PromotionLogics.Models;

public record SetPromotionForProductsRequestModel<T>(PromotionEntity Promotion, List<T> Products) where T : IProductPrice;