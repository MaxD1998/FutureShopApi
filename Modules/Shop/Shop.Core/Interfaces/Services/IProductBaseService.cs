using Shared.Core.Dtos;
using Shared.Shared.Dtos;
using Shop.Core.Dtos;
using Shop.Core.Dtos.ProductBase;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Core.Interfaces.Services;

public interface IProductBaseService
{
    Task<ResultDto<ProductBaseResponseFormDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<IdNameDto>> GetIdNameByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<PageDto<ProductBaseListDto>>> GetPageAsync(PaginationDto pagination, CancellationToken cancellationToken);

    Task<ResultDto<ProductBaseResponseFormDto>> UpdateAsync(Guid id, ProductBaseRequestFormDto dto, CancellationToken cancellationToken);
}
