using Shared.Core.Dtos;
using Shop.Core.Dtos.Basket;
using Shop.Core.Dtos.Basket.BasketItem;
using Shared.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Core.Interfaces.Services;

public interface IBasketService
{
    Task<ResultDto<BasketResponseFormDto>> CreateAsync(BasketRequestFormDto dto, CancellationToken cancellationToken);

    Task<ResultDto<BasketDto>> GetByAuthorizedUserAsync(CancellationToken cancellationToken);

    Task<ResultDto<BasketDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<BasketDto>> ImportPurchaseListAsync(ImportPurchaseListToBasketDto dto, CancellationToken cancellationToken);

    Task<ResultDto<BasketResponseFormDto>> UpdateAsync(Guid id, BasketRequestFormDto dto, CancellationToken cancellationToken);
}
