using Shared.Core.Dtos;
using Shared.Shared.Dtos;
using Shop.Core.Dtos;
using Shop.Core.Dtos.AdCampaign;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Core.Interfaces.Services;

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
