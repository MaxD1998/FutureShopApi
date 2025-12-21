using Shared.Core.Dtos;
using Shared.Shared.Dtos;
using Shop.Core.Dtos;
using Shop.Core.Dtos.Promotion;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Core.Interfaces.Services;

public interface IPromotionService
{
    Task<ResultDto<PromotionResponseFormDto>> CreateAsync(PromotionRequestFormDto dto, CancellationToken cancellationToken);

    Task<ResultDto> DeleteByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<List<string>>> GetActualCodesAsync(CancellationToken cancellationToken);

    Task<ResultDto<PromotionResponseFormDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<List<IdNameDto>>> GetListIdNameAsync(CancellationToken cancellationToken);

    Task<ResultDto<PageDto<PromotionListDto>>> GetPageAsync(PaginationDto pagination, CancellationToken cancellationToken);

    Task<ResultDto<PromotionResponseFormDto>> UpdateAsync(Guid id, PromotionRequestFormDto dto, CancellationToken cancellationToken);
}
