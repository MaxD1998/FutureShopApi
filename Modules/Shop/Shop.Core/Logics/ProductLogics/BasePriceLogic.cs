using Shop.Core.Interfaces.Repositories;

namespace Shop.Core.Logics.ProductLogics;

internal abstract class BasePriceLogic(IProductRepository productRepository)
{
    protected readonly IProductRepository _productRepository = productRepository;

    public async Task<bool> GetProductWasActiveByIdAsync(Guid? productId, CancellationToken cancellationToken)
    {
        var wasActive = false;

        if (productId.HasValue)
        {
            var product = await _productRepository.GetByIdAsync(productId.Value, cancellationToken);

            if (product != null)
                wasActive = product.WasActive;
        }

        return wasActive;
    }
}