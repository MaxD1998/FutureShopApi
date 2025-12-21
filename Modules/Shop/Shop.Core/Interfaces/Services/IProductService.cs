using Shared.Core.Dtos;
using Shared.Shared.Dtos;
using Shop.Core.Dtos;
using Shop.Core.Dtos.Product;
using Shop.Core.Dtos.Product.Price;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Collections.Generic;

namespace Shop.Core.Interfaces.Services;

public interface IProductService
{
    Task<ResultDto<ProductResponseFormDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<ProductDto>> GetDetailsByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<List<IdNameDto>>> GetListIdNameAsync(List<Guid> excludedIds, CancellationToken cancellationToken);

    Task<ResultDto<PageDto<ProductListDto>>> GetPageListAsync(PaginationDto pagination, CancellationToken cancellationToken);

    Task<ResultDto<List<ProductShopListDto>>> GetShopListByCategoryIdAsync(Guid id, ProductShopListFilterRequestDto request, CancellationToken cancellationToken);

    Task<ResultDto<List<SimulatePriceFormDto>>> SimulateAddPriceAsync(SimulatePriceRequestDto request, CancellationToken cancellationToken);

    Task<ResultDto<List<SimulatePriceFormDto>>> SimulateRemovePriceAsync(SimulateRemovePriceRequestDto request, CancellationToken cancellationToken);

    Task<ResultDto<List<SimulatePriceFormDto>>> SimulateUpdatePriceAsync(SimulatePriceRequestDto request, CancellationToken cancellationToken);

    Task<ResultDto<ProductResponseFormDto>> UpdateAsync(Guid id, ProductRequestFormDto dto, CancellationToken cancellationToken);
}
