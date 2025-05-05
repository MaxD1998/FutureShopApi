using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shop.Core.Dtos.ProductParameter;
using Shop.Infrastructure.Repositories;

namespace Shop.Core.Services;

public interface IProductParameterService
{
    Task<ResultDto<List<ProductParameterFlatDto>>> GetListByProductIdAsync(Guid productId, CancellationToken cancellationToken);
}

public class ProductParameterService(IProductParameterRepository productParameterRepository) : BaseService, IProductParameterService
{
    private readonly IProductParameterRepository _productParameterRepository = productParameterRepository;

    public async Task<ResultDto<List<ProductParameterFlatDto>>> GetListByProductIdAsync(Guid productId, CancellationToken cancellationToken)
    {
        var results = await _productParameterRepository.GetListByProductIdAsync(productId, ProductParameterFlatDto.Map(productId), cancellationToken);

        return Success(results);
    }
}