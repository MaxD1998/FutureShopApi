using Shared.Core.Dtos;
using Shop.Core.Dtos.Category;
using Shop.Core.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;
using Shared.Shared.Dtos;

namespace Shop.Core.Interfaces.Services;

public interface ICategoryService
{
    Task<ResultDto<IdNameDto>> GetActiveIdNameByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<List<CategoryListDto>>> GetActiveListAsync(CancellationToken cancellationToken);

    Task<ResultDto<CategoryResponseFormDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<IdNameDto>> GetIdNameByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<PageDto<CategoryPageListDto>>> GetPageListAsync(PaginationDto pagination, CancellationToken cancellationToken);

    Task<ResultDto<CategoryResponseFormDto>> UdpateAsync(Guid id, CategoryRequestFormDto dto, CancellationToken cancellationToken);
}