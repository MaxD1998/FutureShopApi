using Microsoft.AspNetCore.Http;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Core.Extensions;
using Shared.Core.Services;
using Shared.Infrastructure.Constants;
using Shared.Infrastructure.Extensions;
using Shop.Core.Dtos.Product;
using Shop.Domain.Entities;
using Shop.Infrastructure.Models.Product;
using Shop.Infrastructure.Repositories;

namespace Shop.Core.Services;

public interface IProductService
{
    Task<ResultDto> CreateOrUpdateAsync(ProductEventDto dto, CancellationToken cancellationToken);

    Task<ResultDto> DeleteByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<ProductFormDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<ProductDto>> GetDetailsByIdAsync(Guid id, Guid? favouriteId, CancellationToken cancellationToken);

    Task<ResultDto<PageDto<ProductListDto>>> GetPageListAsync(int pageNumber, CancellationToken cancellationToken);

    Task<ResultDto<List<ProductShopListDto>>> GetShopListByCategoryIdAsync(Guid id, ProductShopListFilterRequestDto request, CancellationToken cancellationToken);

    Task<ResultDto<ProductFormDto>> UpdateAsync(Guid id, ProductFormDto dto, CancellationToken cancellationToken);
}

public class ProductService(
    IHeaderService headerService,
    IHttpContextAccessor httpContextAccessor,
    IProductBaseRepository productBaseRepository,
    IProductRepository productRepository
    ) : BaseService, IProductService
{
    private readonly IHeaderService _headerService = headerService;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IProductBaseRepository _productBaseRepository = productBaseRepository;
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<ResultDto> CreateOrUpdateAsync(ProductEventDto dto, CancellationToken cancellationToken)
    {
        var entity = await MapToEntity(dto, cancellationToken);
        await _productRepository.CreateOrUpdateForEventAsync(entity, cancellationToken);

        return Success();
    }

    public async Task<ResultDto> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _productRepository.DeleteByIdAsync(id, cancellationToken);

        return Success();
    }

    public async Task<ResultDto<ProductFormDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _productRepository.GetByIdAsync(id, ProductFormDto.Map(), cancellationToken);

        return Success(result);
    }

    public async Task<ResultDto<ProductDto>> GetDetailsByIdAsync(Guid id, Guid? favouriteId, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.GetUserId();
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
        var userId = _httpContextAccessor.GetUserId();
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

    public async Task<ResultDto<ProductFormDto>> UpdateAsync(Guid id, ProductFormDto dto, CancellationToken cancellationToken)
    {
        var entity = await _productRepository.UpdateAsync(id, dto.ToEntity(), cancellationToken);
        var result = await _productRepository.GetByIdAsync(entity.Id, ProductFormDto.Map(), cancellationToken);

        return Success(result);
    }

    private async Task<ProductEntity> MapToEntity(ProductEventDto dto, CancellationToken cancellationToken)
    {
        var entity = dto.Map();

        var productBaseId = await _productBaseRepository.GetIdByExternalIdAsync(dto.ProductBaseId, cancellationToken);

        if (!productBaseId.HasValue)
            throw new InvalidOperationException();

        entity.ProductBaseId = productBaseId.Value;

        var productId = await _productRepository.GetIdByExternalIdAsync(dto.Id, cancellationToken);

        if (productId.HasValue)
        {
            foreach (var productPhoto in entity.ProductPhotos)
                productPhoto.ProductId = productId.Value;
        }

        return entity;
    }
}