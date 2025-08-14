using Shop.Core.Interfaces;
using Shop.Infrastructure.Repositories;

namespace Shop.Core.Logics.ProductLogics;
internal record GetProductWasActiveModel(Guid? ProductId);

internal class GetProductWasActiveLogic(IProductRepository productRepository) : ILogic<GetProductWasActiveModel, bool>
{
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<bool> ExecuteAsync(GetProductWasActiveModel request, CancellationToken cancellationToken)
    {
        var wasActive = false;

        if (request.ProductId.HasValue)
        {
            var product = await _productRepository.GetByIdAsync(request.ProductId.Value, cancellationToken);

            if (request != null)
            {
                wasActive = product.WasActive;
            }
        }

        return wasActive;
    }
}