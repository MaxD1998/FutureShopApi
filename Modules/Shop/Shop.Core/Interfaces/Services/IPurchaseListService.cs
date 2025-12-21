using Shared.Core.Dtos;
using Shared.Core.Errors;
using Shared.Core.Interfaces.Services;
using Shop.Core.Dtos.PurchaseList;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Core.Interfaces.Services;

public interface IPurchaseListService
{
    Task<ResultDto<PurchaseListResponseFormDto>> CreateAsync(PurchaseListRequestFormDto dto, CancellationToken cancellationToken);

    Task<ResultDto> DeleteByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<PurchaseListDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<List<PurchaseListDto>>> GetListByAuthorizedUserAsync(CancellationToken cancellationToken);

    Task<ResultDto<PurchaseListDto>> ImportBasketAsync(ImportBasketToPurchaseListDto dto, CancellationToken cancellationToken);

    Task<ResultDto<PurchaseListResponseFormDto>> UpdateAsync(Guid id, PurchaseListRequestFormDto dto, CancellationToken cancellationToken);
}
