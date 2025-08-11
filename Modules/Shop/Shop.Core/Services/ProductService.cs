using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Core.Services;
using Shared.Infrastructure.Constants;
using Shared.Infrastructure.Extensions;
using Shop.Core.Dtos;
using Shop.Core.Dtos.Price;
using Shop.Core.Dtos.Product;
using Shop.Core.Factories;
using Shop.Infrastructure.Models.Product;
using Shop.Infrastructure.Repositories;

namespace Shop.Core.Services;

public interface IProductService
{
    Task<ResultDto<ProductFormDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<ProductDto>> GetDetailsByIdAsync(Guid id, Guid? favouriteId, CancellationToken cancellationToken);

    Task<ResultDto<PageDto<ProductListDto>>> GetPageListAsync(int pageNumber, CancellationToken cancellationToken);

    Task<ResultDto<List<ProductShopListDto>>> GetShopListByCategoryIdAsync(Guid id, ProductShopListFilterRequestDto request, CancellationToken cancellationToken);

    Task<ResultDto<List<SimulatePriceFormDto>>> SimulateAddPrice(SimulatePriceRequestDto request, CancellationToken cancellationToken);

    Task<ResultDto<List<SimulatePriceFormDto>>> SimulateRemovePrice(SimulateRemovePriceRequestDto request, CancellationToken cancellationToken);

    Task<ResultDto<List<SimulatePriceFormDto>>> SimulateUpdatePrice(SimulatePriceRequestDto request, CancellationToken cancellationToken);

    Task<ResultDto<ProductFormDto>> UpdateAsync(Guid id, ProductFormDto dto, CancellationToken cancellationToken);
}

public class ProductService(IHeaderService headerService, ICurrentUserService currentUserService, IProductRepository productRepository) : BaseService, IProductService
{
    private readonly ICurrentUserService _currentUserService = currentUserService;
    private readonly IHeaderService _headerService = headerService;
    private readonly ILogicFactory _logicFactory = new LogicFactory();
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<ResultDto<ProductFormDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _productRepository.GetByIdAsync(id, ProductFormDto.Map(), cancellationToken);

        return Success(result);
    }

    public async Task<ResultDto<ProductDto>> GetDetailsByIdAsync(Guid id, Guid? favouriteId, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.GetUserId();
        var result = await _productRepository.GetByIdAsync(id, ProductDto.Map(_headerService.GetHeader(HeaderNameConst.Lang), userId, favouriteId), cancellationToken);

        return Success(result);
    }

    public async Task<ResultDto<PageDto<ProductListDto>>> GetPageListAsync(int pageNumber, CancellationToken cancellationToken)
    {
        var results = await _productRepository.GetPageAsync(pageNumber, ProductListDto.Map(), cancellationToken);

        return Success(results);
    }

    public async Task<ResultDto<List<ProductShopListDto>>> GetShopListByCategoryIdAsync(Guid id, ProductShopListFilterRequestDto request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.GetUserId();
        var lang = _headerService.GetHeader(HeaderNameConst.Lang);
        var parameters = new GetProductListByCategoryIdParams
        {
            CategoryId = id,
            Filter = request,
            Lang = lang,
            UserId = userId
        };

        var results = await _productRepository.GetListByCategoryIdAsync(parameters, ProductShopListDto.Map(lang, userId, request.FavouriteId), cancellationToken);

        return Success(results);
    }

    public async Task<ResultDto<List<SimulatePriceFormDto>>> SimulateAddPrice(SimulatePriceRequestDto request, CancellationToken cancellationToken)
        => await _logicFactory.ExecuteAsync(request, f => f.CreateSimulateAddPriceLogic(_productRepository), cancellationToken);

    public async Task<ResultDto<List<SimulatePriceFormDto>>> SimulateRemovePrice(SimulateRemovePriceRequestDto request, CancellationToken cancellationToken)
        => await _logicFactory.ExecuteAsync(request, f => f.CreateSimulateRemovePriceLogic(_productRepository), cancellationToken);

    public async Task<ResultDto<List<SimulatePriceFormDto>>> SimulateUpdatePrice(SimulatePriceRequestDto request, CancellationToken cancellationToken)
        => await _logicFactory.ExecuteAsync(request, f => f.CreateSimulateUpdatePriceLogic(_productRepository), cancellationToken);

    public async Task<ResultDto<ProductFormDto>> UpdateAsync(Guid id, ProductFormDto dto, CancellationToken cancellationToken)
    {
        var entity = await _productRepository.UpdateAsync(id, dto.ToEntity(), cancellationToken);
        var result = await _productRepository.GetByIdAsync(entity.Id, ProductFormDto.Map(), cancellationToken);

        return Success(result);
    }
}