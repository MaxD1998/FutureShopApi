using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Core.Services;
using Shared.Infrastructure.Constants;
using Shared.Infrastructure.Extensions;
using Shared.Shared.Dtos;
using Shared.Shared.Extensions;
using Shop.Core.Dtos;
using Shop.Core.Dtos.Product;
using Shop.Core.Dtos.Product.Price;
using Shop.Core.Factories;
using Shop.Core.Logics.PromotionLogics;
using Shop.Infrastructure.Models.Products;
using Shop.Infrastructure.Persistence.Repositories;

namespace Shop.Core.Services;

public interface IProductService
{
    Task<ResultDto<ProductResponseFormDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<ProductDto>> GetDetailsByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<List<IdNameDto>>> GetListIdNameAsync(List<Guid> excludedIds, CancellationToken cancellationToken);

    Task<ResultDto<PageDto<ProductListDto>>> GetPageListAsync(PaginationDto pagination, CancellationToken cancellationToken);

    Task<ResultDto<List<ProductShopListDto>>> GetShopListByCategoryIdAsync(Guid id, ProductShopListFilterRequestDto request, CancellationToken cancellationToken);

    Task<ResultDto<List<SimulatePriceFormDto>>> SimulateAddPriceAsync(SimulatePriceRequestDto request, CancellationToken cancellationToken);

    Task<ResultDto<List<SimulatePriceFormDto>>> SimulateRemovePriceAsync(SimulateRemovePriceRequestDto request, CancellationToken cancellationToken);

    Task<ResultDto<List<SimulatePriceFormDto>>> SimulateUpdatePriceAsync(SimulatePriceRequestDto request, CancellationToken cancellationToken);

    Task<ResultDto<ProductResponseFormDto>> UpdateAsync(Guid id, ProductRequestFormDto dto, CancellationToken cancellationToken);
}

internal class ProductService(
    IHeaderService headerService,
    ICurrentUserService currentUserService,
    ILogicFactory logicFactory,
    IProductRepository productRepository,
    IPromotionRepository promotionRepository) : IProductService
{
    private readonly ICurrentUserService _currentUserService = currentUserService;
    private readonly IHeaderService _headerService = headerService;
    private readonly ILogicFactory _logicFactory = logicFactory;
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IPromotionRepository _promotionRepository = promotionRepository;

    public async Task<ResultDto<ProductResponseFormDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _productRepository.GetByIdAsync(id, ProductResponseFormDto.Map(), cancellationToken);

        return ResultDto.Success(result);
    }

    public async Task<ResultDto<ProductDto>> GetDetailsByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.GetUserId();
        var lang = _headerService.GetHeader(HeaderNameConst.Lang);
        var favouriteId = _headerService.GetHeader(HeaderNameConst.FavouriteId).ToNullableGuid();
        var codes = _headerService.GetHeader(HeaderNameConst.Codes).ToListString();
        var result = await _productRepository.GetByIdAsync(id, ProductDto.Map(lang, userId, favouriteId), cancellationToken);

        var results = new List<ProductDto>()
        {
            result
        };

        var promotionRequest = new SetPromotionForProductsRequestModel<ProductDto>(codes, results);
        results = await _logicFactory.ExecuteAsync(promotionRequest, f => f.SetPromotionForProductsLogic<ProductDto>(), cancellationToken);

        var promotions = await _promotionRepository.GetActivePromotionsAsync(codes, cancellationToken);

        return ResultDto.Success(result);
    }

    public async Task<ResultDto<List<IdNameDto>>> GetListIdNameAsync(List<Guid> excludedIds, CancellationToken cancellationToken)
    {
        var results = await _productRepository.GetListAsync(x => !excludedIds.Contains(x.Id), IdNameDto.MapFromProduct(), cancellationToken);

        return ResultDto.Success(results);
    }

    public async Task<ResultDto<PageDto<ProductListDto>>> GetPageListAsync(PaginationDto pagination, CancellationToken cancellationToken)
    {
        var results = await _productRepository.GetPageAsync(pagination, ProductListDto.Map(), cancellationToken);

        return ResultDto.Success(results);
    }

    public async Task<ResultDto<List<ProductShopListDto>>> GetShopListByCategoryIdAsync(Guid id, ProductShopListFilterRequestDto request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.GetUserId();
        var lang = _headerService.GetHeader(HeaderNameConst.Lang);
        var favouriteId = _headerService.GetHeader(HeaderNameConst.FavouriteId).ToNullableGuid();
        var codes = _headerService.GetHeader(HeaderNameConst.Codes).ToListString();
        var parameters = new GetProductListByCategoryIdParams
        {
            CategoryId = id,
            Filter = request,
            Lang = lang,
            UserId = userId
        };

        var results = await _productRepository.GetListByCategoryIdAsync(parameters, ProductShopListDto.Map(lang, userId, favouriteId), cancellationToken);
        var promotionRequest = new SetPromotionForProductsRequestModel<ProductShopListDto>(codes, results);
        results = await _logicFactory.ExecuteAsync(promotionRequest, f => f.SetPromotionForProductsLogic<ProductShopListDto>(), cancellationToken);

        return ResultDto.Success(results);
    }

    public async Task<ResultDto<List<SimulatePriceFormDto>>> SimulateAddPriceAsync(SimulatePriceRequestDto request, CancellationToken cancellationToken)
        => await _logicFactory.ExecuteAsync(request, f => f.SimulateAddPriceLogic(), cancellationToken);

    public async Task<ResultDto<List<SimulatePriceFormDto>>> SimulateRemovePriceAsync(SimulateRemovePriceRequestDto request, CancellationToken cancellationToken)
        => await _logicFactory.ExecuteAsync(request, f => f.SimulateRemovePriceLogic(), cancellationToken);

    public async Task<ResultDto<List<SimulatePriceFormDto>>> SimulateUpdatePriceAsync(SimulatePriceRequestDto request, CancellationToken cancellationToken)
        => await _logicFactory.ExecuteAsync(request, f => f.SimulateUpdatePriceLogic(), cancellationToken);

    public async Task<ResultDto<ProductResponseFormDto>> UpdateAsync(Guid id, ProductRequestFormDto dto, CancellationToken cancellationToken)
    {
        var entity = await _productRepository.UpdateAsync(id, dto.ToEntity(), cancellationToken);
        var result = await _productRepository.GetByIdAsync(entity.Id, ProductResponseFormDto.Map(), cancellationToken);

        return ResultDto.Success(result);
    }
}