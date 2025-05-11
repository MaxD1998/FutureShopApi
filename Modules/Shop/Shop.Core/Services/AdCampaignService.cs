using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Infrastructure.Extensions;
using Shop.Core.Dtos.AdCampaign;
using Shop.Infrastructure.Repositories;

namespace Shop.Core.Services;

public interface IAdCampaignService
{
    Task<ResultDto<AdCampaignFormDto>> CreateAsync(AdCampaignFormDto dto, CancellationToken cancellationToken);

    Task<ResultDto> DeleteByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<List<string>>> GetActualAsync(CancellationToken cancellationToken);

    Task<ResultDto<AdCampaignFormDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<PageDto<AdCampaignListDto>>> GetPageAsync(int pageIndex, CancellationToken cancellationToken);

    Task<ResultDto<AdCampaignFormDto>> UpdateAsync(Guid id, AdCampaignFormDto dto, CancellationToken cancellationToken);
}

public class AdCampaignService(IAdCampaignRepository adCampaignRepository) : BaseService, IAdCampaignService
{
    private readonly IAdCampaignRepository _adCampaignRepository = adCampaignRepository;

    public async Task<ResultDto<AdCampaignFormDto>> CreateAsync(AdCampaignFormDto dto, CancellationToken cancellationToken)
    {
        var entity = await _adCampaignRepository.CreateAsync(dto.ToEntity(), cancellationToken);
        var result = await _adCampaignRepository.GetByIdAsync(entity.Id, AdCampaignFormDto.Map(), cancellationToken);

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

    public async Task<ResultDto<AdCampaignFormDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _adCampaignRepository.GetByIdAsync(id, AdCampaignFormDto.Map(), cancellationToken);

        return Success(result);
    }

    public async Task<ResultDto<PageDto<AdCampaignListDto>>> GetPageAsync(int pageIndex, CancellationToken cancellationToken)
    {
        var results = await _adCampaignRepository.GetPageAsync(pageIndex, AdCampaignListDto.Map(), cancellationToken);

        return Success(results);
    }

    public async Task<ResultDto<AdCampaignFormDto>> UpdateAsync(Guid id, AdCampaignFormDto dto, CancellationToken cancellationToken)
    {
        var entity = await _adCampaignRepository.UpdateAsync(id, dto.ToEntity(), cancellationToken);
        var result = await _adCampaignRepository.GetByIdAsync(entity.Id, AdCampaignFormDto.Map(), cancellationToken);

        return Success(result);
    }
}