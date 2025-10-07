using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Core.Services;
using Shared.Infrastructure.Constants;
using Shared.Infrastructure.Extensions;
using Shop.Core.Dtos;
using Shop.Core.Dtos.Product;
using Shop.Core.Dtos.Product.Price;
using Shop.Core.Factories;
using Shop.Core.Logics.PromotionLogics.Models;
using Shop.Infrastructure.Enums;
using Shop.Infrastructure.Models.Products;
using Shop.Infrastructure.Repositories;
using System.Text.Json;

namespace Shop.Core.Services;

public interface IProductService
{
    Task<ResultDto<ProductResponseFormDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<ProductDto>> GetDetailsByIdAsync(Guid id, Guid? favouriteId, CancellationToken cancellationToken);

    Task<ResultDto<List<IdNameDto>>> GetListIdNameAsync(List<Guid> excludedIds, CancellationToken cancellationToken);

    Task<ResultDto<PageDto<ProductListDto>>> GetPageListAsync(int pageNumber, CancellationToken cancellationToken);

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
    IPromotionRepository promotionRepository) : BaseService, IProductService
{
    private readonly ICurrentUserService _currentUserService = currentUserService;
    private readonly IHeaderService _headerService = headerService;
    private readonly ILogicFactory _logicFactory = logicFactory;
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IPromotionRepository _promotionRepository = promotionRepository;

    public async Task<ResultDto<ProductResponseFormDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _productRepository.GetByIdAsync(id, ProductResponseFormDto.Map(), cancellationToken);

        return Success(result);
    }

    public async Task<ResultDto<ProductDto>> GetDetailsByIdAsync(Guid id, Guid? favouriteId, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.GetUserId();
        var lang = _headerService.GetHeader(HeaderNameConst.Lang);
        var codesStr = _headerService.GetHeader(HeaderNameConst.Codes);
        var codes = JsonSerializer.Deserialize<List<string>>(codesStr);
        var result = await _productRepository.GetByIdAsync(id, ProductDto.Map(lang, userId, favouriteId), cancellationToken);

        var promotions = await _promotionRepository.GetActivePromotionsAsync(codes, cancellationToken);

        if (promotions != null && promotions.Count > 0)
        {
            var results = new List<ProductDto>()
            {
                result
            };

            foreach (var promotion in promotions)
            {
                var promotionRequest = new SetPromotionForProductsRequestModel<ProductDto>(promotion, results);
                results = promotion.Type switch
                {
                    PromotionType.Percent => await _logicFactory.ExecuteAsync(promotionRequest, f => f.SetPercentPromotionForProductsLogic<ProductDto>(), cancellationToken),
                    _ => results
                };
            }
        }

        return Success(result);
    }

    public async Task<ResultDto<List<IdNameDto>>> GetListIdNameAsync(List<Guid> excludedIds, CancellationToken cancellationToken)
    {
        var results = await _productRepository.GetListAsync(x => !excludedIds.Contains(x.Id), IdNameDto.MapFromProduct(), cancellationToken);

        return Success(results);
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
        var codesStr = _headerService.GetHeader(HeaderNameConst.Codes);
        var codes = JsonSerializer.Deserialize<List<string>>(codesStr);
        var parameters = new GetProductListByCategoryIdParams
        {
            CategoryId = id,
            Filter = request,
            Lang = lang,
            UserId = userId
        };

        var results = await _productRepository.GetListByCategoryIdAsync(parameters, ProductShopListDto.Map(lang, userId, request.FavouriteId), cancellationToken);
        var promotions = await _promotionRepository.GetActivePromotionsAsync(codes, cancellationToken);

        if (promotions != null && promotions.Count > 0)
        {
            foreach (var promotion in promotions)
            {
                var promotionRequest = new SetPromotionForProductsRequestModel<ProductShopListDto>(promotion, results);
                results = promotion.Type switch
                {
                    PromotionType.Percent => await _logicFactory.ExecuteAsync(promotionRequest, f => f.SetPercentPromotionForProductsLogic<ProductShopListDto>(), cancellationToken),
                    _ => results
                };
            }
        }

        return Success(results);
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

        return Success(result);
    }
}