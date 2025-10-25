using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Core.Services;
using Shared.Infrastructure.Extensions;
using Shared.Shared.Dtos;
using Shop.Core.Dtos.ProductReview;
using Shop.Infrastructure.Repositories;

namespace Shop.Core.Services;

public interface IProductReviewService
{
    Task<ResultDto<ProductReviewResponseFormDto>> CreateAsync(ProductReviewRequestFormDto dto, CancellationToken cancellationToken);

    Task<ResultDto> DeleteByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<PageDto<ProductReviewResponseFormDto>>> GetPageByProductIdAsync(Guid productId, PaginationDto pagination, CancellationToken cancellationToken);

    Task<ResultDto<ProductReviewResponseFormDto>> UpdateAsync(Guid id, ProductReviewRequestFormDto dto, CancellationToken cancellationToken);
}

internal class ProductReviewService(ICurrentUserService currentUserService, IProductReviewRepository productReviewRepository) : BaseService, IProductReviewService
{
    private readonly ICurrentUserService _currentUserService = currentUserService;
    private readonly IProductReviewRepository _productReviewRepository = productReviewRepository;

    public async Task<ResultDto<ProductReviewResponseFormDto>> CreateAsync(ProductReviewRequestFormDto dto, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.GetUserId() ?? Guid.Empty;
        var entity = await _productReviewRepository.CreateAsync(dto.ToEntity(userId), cancellationToken);
        var result = await _productReviewRepository.GetByIdAsync(entity.Id, ProductReviewResponseFormDto.Map(), cancellationToken);

        return Success(result);
    }

    public async Task<ResultDto> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _productReviewRepository.DeleteByIdAsync(id, cancellationToken);
        return Success();
    }

    public async Task<ResultDto<PageDto<ProductReviewResponseFormDto>>> GetPageByProductIdAsync(Guid productId, PaginationDto pagination, CancellationToken cancellationToken)
    {
        var results = await _productReviewRepository.GetPageAsync(pagination, x => x.ProductId == productId, ProductReviewResponseFormDto.Map(), cancellationToken);
        return Success(results);
    }

    public async Task<ResultDto<ProductReviewResponseFormDto>> UpdateAsync(Guid id, ProductReviewRequestFormDto dto, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.GetUserId() ?? Guid.Empty;
        var entity = await _productReviewRepository.UpdateAsync(id, dto.ToEntity(userId), cancellationToken);
        var result = await _productReviewRepository.GetByIdAsync(entity.Id, ProductReviewResponseFormDto.Map(), cancellationToken);

        return Success(result);
    }
}