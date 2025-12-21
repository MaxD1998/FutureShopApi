using Product.Core.Dtos;
using Product.Core.Dtos.ProductBase;
using Shared.Core.Dtos;
using Shared.Shared.Dtos;

namespace Product.Core.Interfaces.Services;

public interface IProductBaseService
{
    Task<ResultDto<ProductBaseResponseFormDto>> CreateAsync(ProductBaseRequestFormDto dto, CancellationToken cancellationToken);

    Task<ResultDto> DeleteAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<ProductBaseResponseFormDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<IdNameDto>> GetIdNameByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<List<IdNameDto>>> GetListIdNameAsync(CancellationToken cancellationToken);

    Task<ResultDto<PageDto<ProductBaseListDto>>> GetPageListAsync(PaginationDto pagination, CancellationToken cancellationToken);

    Task<ResultDto<ProductBaseResponseFormDto>> UpdateAsync(Guid id, ProductBaseRequestFormDto dto, CancellationToken cancellationToken);
}