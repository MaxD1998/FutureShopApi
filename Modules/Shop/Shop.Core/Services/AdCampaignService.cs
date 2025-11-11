using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Core.Services;
using Shared.Infrastructure.Constants;
using Shared.Infrastructure.Extensions;
using Shared.Shared.Dtos;
using Shared.Shared.Extensions;
using Shop.Core.Dtos;
using Shop.Core.Dtos.AdCampaign;
using Shop.Core.Dtos.Product;
using Shop.Core.Factories;
using Shop.Core.Logics.PromotionLogics;
using Shop.Infrastructure.Repositories;

namespace Shop.Core.Services;

public interface IAdCampaignService
{
    Task<ResultDto<AdCampaignResponseFormDto>> CreateAsync(AdCampaignRequestFormDto dto, CancellationToken cancellationToken);

    Task<ResultDto> DeleteByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<List<IdFileIdDto>>> GetActualAsync(CancellationToken cancellationToken);

    Task<ResultDto<AdCampaignDto>> GetActualByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<AdCampaignResponseFormDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<List<IdNameDto>>> GetListIdNameAsync(CancellationToken cancellationToken);

    Task<ResultDto<PageDto<AdCampaignListDto>>> GetPageAsync(PaginationDto pagination, CancellationToken cancellationToken);

    Task<ResultDto<AdCampaignResponseFormDto>> UpdateAsync(Guid id, AdCampaignRequestFormDto dto, CancellationToken cancellationToken);
}

internal class AdCampaignService(
    IAdCampaignRepository adCampaignRepository,
    ICurrentUserService currentUserService,
    IHeaderService headerService,
    ILogicFactory logicFactory,
    IPromotionRepository promotionRepository) : IAdCampaignService
{
    private readonly IAdCampaignRepository _adCampaignRepository = adCampaignRepository;
    private readonly ICurrentUserService _currentUserService = currentUserService;
    private readonly IHeaderService _headerService = headerService;
    private readonly ILogicFactory _logicFactory = logicFactory;
    private readonly IPromotionRepository _promotionRepository = promotionRepository;

    public async Task<ResultDto<AdCampaignResponseFormDto>> CreateAsync(AdCampaignRequestFormDto dto, CancellationToken cancellationToken)
    {
        var entity = dto.ToEntity();

        if (dto.PromotionId.HasValue && dto.PromotionId != Guid.Empty)
            entity.Promotion = await _promotionRepository.GetByIdAsync(dto.PromotionId.Value, cancellationToken);

        entity = await _adCampaignRepository.CreateAsync(entity, cancellationToken);
        var result = await _adCampaignRepository.GetByIdAsync(entity.Id, AdCampaignResponseFormDto.Map(), cancellationToken);

        return ResultDto.Success(result);
    }

    public async Task<ResultDto> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _adCampaignRepository.DeleteByIdAsync(id, cancellationToken);
        return ResultDto.Success();
    }

    public async Task<ResultDto<List<IdFileIdDto>>> GetActualAsync(CancellationToken cancellationToken)
    {
        var lang = _headerService.GetHeader(HeaderNameConst.Lang);
        var results = await _adCampaignRepository.GetActualAsync(IdFileIdDto.MapFromAdCampaign(lang), cancellationToken);

        results = results.Where(x => !string.IsNullOrEmpty(x.FileId)).ToList();

        return ResultDto.Success(results);
    }

    public async Task<ResultDto<AdCampaignDto>> GetActualByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.GetUserId();
        var lang = _headerService.GetHeader(HeaderNameConst.Lang);
        var favouriteId = _headerService.GetHeader(HeaderNameConst.FavouriteId).ToNullableGuid();

        var result = await _adCampaignRepository.GetActualByIdAsync(id, AdCampaignDto.Map(lang, userId, favouriteId), cancellationToken);

        if (string.IsNullOrEmpty(result.FileId))
            return ResultDto.Success<AdCampaignDto>(null);

        switch (result.Type)
        {
            case Infrastructure.Enums.AdCampaignType.Product:
            {
                var codes = _headerService.GetHeader(HeaderNameConst.Codes).ToListString();
                var request = new SetPromotionForProductsRequestModel<ProductShopListDto>(codes, result.Products);
                result.Products = await _logicFactory.ExecuteAsync(request, f => f.SetPromotionForProductsLogic<ProductShopListDto>(), cancellationToken);
                break;
            }

            case Infrastructure.Enums.AdCampaignType.Promotion:
            {
                var request = new SetPromotionForProductsRequestModel<ProductShopListDto>([result.Promotion.Code], result.Promotion.Products);
                result.Promotion.Products = await _logicFactory.ExecuteAsync(request, f => f.SetPromotionForProductsLogic<ProductShopListDto>(), cancellationToken);
                break;
            }

            default:
                break;
        }

        return ResultDto.Success(result);
    }

    public async Task<ResultDto<AdCampaignResponseFormDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _adCampaignRepository.GetByIdAsync(id, AdCampaignResponseFormDto.Map(), cancellationToken);
        return ResultDto.Success(result);
    }

    public async Task<ResultDto<List<IdNameDto>>> GetListIdNameAsync(CancellationToken cancellationToken)
    {
        var results = await _adCampaignRepository.GetListAsync(IdNameDto.MapFromAdCampaign(), cancellationToken);
        return ResultDto.Success(results);
    }

    public async Task<ResultDto<PageDto<AdCampaignListDto>>> GetPageAsync(PaginationDto pagination, CancellationToken cancellationToken)
    {
        var results = await _adCampaignRepository.GetPageAsync(pagination, AdCampaignListDto.Map(), cancellationToken);
        return ResultDto.Success(results);
    }

    public async Task<ResultDto<AdCampaignResponseFormDto>> UpdateAsync(Guid id, AdCampaignRequestFormDto dto, CancellationToken cancellationToken)
    {
        var entity = dto.ToEntity();

        if (dto.PromotionId.HasValue && dto.PromotionId != Guid.Empty)
            entity.Promotion = await _promotionRepository.GetByIdAsync(dto.PromotionId.Value, cancellationToken);

        entity = await _adCampaignRepository.UpdateAsync(id, entity, cancellationToken);
        var result = await _adCampaignRepository.GetByIdAsync(entity.Id, AdCampaignResponseFormDto.Map(), cancellationToken);

        return ResultDto.Success(result);
    }
}