using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Core.Services;
using Shared.Infrastructure.Extensions;
using Shared.Shared.Dtos;
using Shop.Core.Dtos.ProductReview;
using Shop.Core.Errors;
using Shop.Infrastructure.Repositories;
using System.Net;

namespace Shop.Core.Services;

public interface IProductReviewService
{
    Task<ResultDto<ProductReviewResponseFormDto>> CreateAsync(ProductReviewRequestFormDto dto, CancellationToken cancellationToken);

    Task<ResultDto> DeleteByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<PageDto<ProductReviewResponseFormDto>>> GetPageByProductIdAsync(Guid productId, PaginationDto pagination, CancellationToken cancellationToken);

    Task<ResultDto<ProductReviewResponseFormDto>> UpdateAsync(Guid id, ProductReviewRequestFormDto dto, CancellationToken cancellationToken);
}

internal class ProductReviewService(ICurrentUserService currentUserService, IProductReviewRepository productReviewRepository) : IProductReviewService
{
    private readonly ICurrentUserService _currentUserService = currentUserService;
    private readonly IProductReviewRepository _productReviewRepository = productReviewRepository;

    public async Task<ResultDto<ProductReviewResponseFormDto>> CreateAsync(ProductReviewRequestFormDto dto, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.GetUserId() ?? Guid.Empty;
        var hasReview = await _productReviewRepository.AnyByUserIdAndProductIdAsync(userId, dto.ProductId, cancellationToken);

        if (hasReview)
            return ResultDto.Error<ProductReviewResponseFormDto>(HttpStatusCode.BadRequest, ExceptionMessage.ProductReview001UserCreatedReviewForThisProduct);

        var entity = await _productReviewRepository.CreateAsync(dto.ToEntity(userId), cancellationToken);
        var result = await _productReviewRepository.GetByIdAsync(entity.Id, ProductReviewResponseFormDto.Map(), cancellationToken);

        return ResultDto.Success(result);
    }

    public async Task<ResultDto> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _productReviewRepository.DeleteByIdAsync(id, cancellationToken);
        return ResultDto.Success();
    }

    public async Task<ResultDto<PageDto<ProductReviewResponseFormDto>>> GetPageByProductIdAsync(Guid productId, PaginationDto pagination, CancellationToken cancellationToken)
    {
        var results = await _productReviewRepository.GetPageAsync(pagination, x => x.ProductId == productId, ProductReviewResponseFormDto.Map(), cancellationToken);
        return ResultDto.Success(results);
    }

    public async Task<ResultDto<ProductReviewResponseFormDto>> UpdateAsync(Guid id, ProductReviewRequestFormDto dto, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.GetUserId() ?? Guid.Empty;
        var productReviewUserId = await _productReviewRepository.GetByIdAsync(id, x => x.UserId, cancellationToken);

        if (userId != productReviewUserId)
            return ResultDto.Error<ProductReviewResponseFormDto>(HttpStatusCode.BadRequest, ExceptionMessage.ProductReview002UserIsNotAuthorizedToUpdateThisReview);

        var entity = await _productReviewRepository.UpdateAsync(id, dto.ToEntity(userId), cancellationToken);
        var result = await _productReviewRepository.GetByIdAsync(entity.Id, ProductReviewResponseFormDto.Map(), cancellationToken);

        return ResultDto.Success(result);
    }
}