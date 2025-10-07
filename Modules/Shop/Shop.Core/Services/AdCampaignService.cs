using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Infrastructure.Extensions;
using Shop.Core.Dtos;
using Shop.Core.Dtos.AdCampaign;
using Shop.Infrastructure.Repositories;

namespace Shop.Core.Services;

public interface IAdCampaignService
{
    Task<ResultDto<AdCampaignResponseFormDto>> CreateAsync(AdCampaignRequestFormDto dto, CancellationToken cancellationToken);

    Task<ResultDto> DeleteByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<List<string>>> GetActualAsync(CancellationToken cancellationToken);

    Task<ResultDto<AdCampaignResponseFormDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<List<IdNameDto>>> GetListIdNameAsync(CancellationToken cancellationToken);

    Task<ResultDto<PageDto<AdCampaignListDto>>> GetPageAsync(int pageIndex, CancellationToken cancellationToken);

    Task<ResultDto<AdCampaignResponseFormDto>> UpdateAsync(Guid id, AdCampaignRequestFormDto dto, CancellationToken cancellationToken);
}

internal class AdCampaignService(IAdCampaignRepository adCampaignRepository) : BaseService, IAdCampaignService
{
    private readonly IAdCampaignRepository _adCampaignRepository = adCampaignRepository;

    public async Task<ResultDto<AdCampaignResponseFormDto>> CreateAsync(AdCampaignRequestFormDto dto, CancellationToken cancellationToken)
    {
        var entity = await _adCampaignRepository.CreateAsync(dto.ToEntity(), cancellationToken);
        var result = await _adCampaignRepository.GetByIdAsync(entity.Id, AdCampaignResponseFormDto.Map(), cancellationToken);

        return Success(result);
    }

    public async Task<ResultDto> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _adCampaignRepository.DeleteByIdAsync(id, cancellationToken);

        return Success();
    }

    public async Task<ResultDto<List<string>>> GetActualAsync(CancellationToken cancellationToken)
    {
        var fileIds = await _adCampaignRepository.GetActualAsync(x => x.AdCampaignItems.AsQueryable().Select(x => x.FileId).ToList(), cancellationToken);
        var results = fileIds.SelectMany(x => x).ToList();
        return Success(results);
    }

    public async Task<ResultDto<AdCampaignResponseFormDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _adCampaignRepository.GetByIdAsync(id, AdCampaignResponseFormDto.Map(), cancellationToken);

        return Success(result);
    }

    public async Task<ResultDto<List<IdNameDto>>> GetListIdNameAsync(CancellationToken cancellationToken)
    {
        var results = await _adCampaignRepository.GetListAsync(IdNameDto.MapFromAdCampaign(), cancellationToken);
        return Success(results);
    }

    public async Task<ResultDto<PageDto<AdCampaignListDto>>> GetPageAsync(int pageIndex, CancellationToken cancellationToken)
    {
        var results = await _adCampaignRepository.GetPageAsync(pageIndex, AdCampaignListDto.Map(), cancellationToken);

        return Success(results);
    }

    public async Task<ResultDto<AdCampaignResponseFormDto>> UpdateAsync(Guid id, AdCampaignRequestFormDto dto, CancellationToken cancellationToken)
    {
        var entity = await _adCampaignRepository.UpdateAsync(id, dto.ToEntity(), cancellationToken);
        var result = await _adCampaignRepository.GetByIdAsync(entity.Id, AdCampaignResponseFormDto.Map(), cancellationToken);

        return Success(result);
    }
}