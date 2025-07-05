using Shop.Core.Interfaces;
using Shop.Infrastructure.Repositories;

namespace Shop.Core.Logics.ProductLogics;

internal class GetProductWasActiveLogic(IProductRepository productRepository) : ILogic<Guid?, bool>
{
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<bool> ExecuteAsync(Guid? request, CancellationToken cancellationToken)
    {
        var wasActive = false;

        if (request != null)
        {
            var product = await _productRepository.GetByIdAsync((Guid)request, cancellationToken);

            if (request != null)
            {
                wasActive = product.WasActive;
            }
        }

        return wasActive;
    }
}