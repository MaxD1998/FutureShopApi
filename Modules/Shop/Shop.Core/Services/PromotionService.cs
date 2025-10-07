using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Infrastructure.Extensions;
using Shop.Core.Dtos;
using Shop.Core.Dtos.Promotion;
using Shop.Infrastructure.Repositories;

namespace Shop.Core.Services;

public interface IPromotionService
{
    Task<ResultDto<PromotionResponseFormDto>> CreateAsync(PromotionRequestFormDto dto, CancellationToken cancellationToken);

    Task<ResultDto> DeleteByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<List<string>>> GetActualCodesAsync(CancellationToken cancellationToken);

    Task<ResultDto<PromotionResponseFormDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<List<IdNameDto>>> GetListIdNameAsync(CancellationToken cancellationToken);

    Task<ResultDto<PageDto<PromotionListDto>>> GetPageAsync(int indexPage, CancellationToken cancellationToken);

    Task<ResultDto<PromotionResponseFormDto>> UpdateAsync(Guid id, PromotionRequestFormDto dto, CancellationToken cancellationToken);
}

internal class PromotionService(IPromotionRepository promotionRepository) : BaseService, IPromotionService
{
    private readonly IPromotionRepository _promotionRepository = promotionRepository;

    public async Task<ResultDto<PromotionResponseFormDto>> CreateAsync(PromotionRequestFormDto dto, CancellationToken cancellationToken)
    {
        var entity = await _promotionRepository.CreateAsync(dto.ToEntity(), cancellationToken);
        var result = await _promotionRepository.GetByIdAsync(entity.Id, PromotionResponseFormDto.Map(), cancellationToken);

        return Success(result);
    }

    public async Task<ResultDto> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _promotionRepository.DeleteByIdAsync(id, cancellationToken);

        return Success();
    }

    public async Task<ResultDto<List<string>>> GetActualCodesAsync(CancellationToken cancellationToken)
    {
        var today = DateTime.UtcNow;
        var results = await _promotionRepository.GetListAsync(x => x.IsActive && x.Start <= today && today < x.End, x => x.Code, cancellationToken);

        return Success(results);
    }

    public async Task<ResultDto<PromotionResponseFormDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _promotionRepository.GetByIdAsync(id, PromotionResponseFormDto.Map(), cancellationToken);

        return Success(result);
    }

    public async Task<ResultDto<List<IdNameDto>>> GetListIdNameAsync(CancellationToken cancellationToken)
    {
        var results = await _promotionRepository.GetListAsync(IdNameDto.MapFromPromotion(), cancellationToken);

        return Success(results);
    }

    public async Task<ResultDto<PageDto<PromotionListDto>>> GetPageAsync(int indexPage, CancellationToken cancellationToken)
    {
        var results = await _promotionRepository.GetPageAsync(indexPage, PromotionListDto.Map(), cancellationToken);

        return Success(results);
    }

    public async Task<ResultDto<PromotionResponseFormDto>> UpdateAsync(Guid id, PromotionRequestFormDto dto, CancellationToken cancellationToken)
    {
        var entity = await _promotionRepository.UpdateAsync(id, dto.ToEntity(), cancellationToken);
        var result = await _promotionRepository.GetByIdAsync(entity.Id, PromotionResponseFormDto.Map(), cancellationToken);

        return Success(result);
    }
}