using Product.Core.Dtos.Product;
using Shared.Core.Dtos;
using System.Threading.Tasks;
using System.Threading;
using System;
using Shared.Shared.Dtos;

namespace Product.Core.Interfaces.Services;

public interface IProductService
{
    Task<ResultDto<ProductResponseFormDto>> CreateAsync(ProductRequestFormDto dto, CancellationToken cancellationToken);

    Task<ResultDto> DeleteByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<ProductResponseFormDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<PageDto<ProductListDto>>> GetPageListAsync(PaginationDto pagination, CancellationToken cancellationToken);

    Task<ResultDto<ProductResponseFormDto>> UpdateAsync(Guid id, ProductRequestFormDto dto, CancellationToken cancellationToken);
}
