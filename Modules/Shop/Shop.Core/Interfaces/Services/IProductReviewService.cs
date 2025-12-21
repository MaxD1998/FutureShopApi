using Shared.Core.Dtos;
using Shared.Shared.Dtos;
using Shop.Core.Dtos.ProductReview;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Core.Interfaces.Services;

public interface IProductReviewService
{
    Task<ResultDto<ProductReviewResponseFormDto>> CreateAsync(ProductReviewRequestFormDto dto, CancellationToken cancellationToken);

    Task<ResultDto> DeleteByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<PageDto<ProductReviewResponseFormDto>>> GetPageByProductIdAsync(Guid productId, PaginationDto pagination, CancellationToken cancellationToken);

    Task<ResultDto<ProductReviewResponseFormDto>> UpdateAsync(Guid id, ProductReviewRequestFormDto dto, CancellationToken cancellationToken);
}
