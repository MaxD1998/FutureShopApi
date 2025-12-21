using Shared.Core.Dtos;
using Shop.Core.Dtos.User.UserDeliveryAddress;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Core.Interfaces.Services;

public interface IUserDeliveryAddressService
{
    Task<ResultDto<UserDeliveryAddressResponseFormDto>> CreateAsync(UserDeliveryAddressRequestFormDto dto, CancellationToken cancellationToken);

    Task<ResultDto> DeleteByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<List<UserDeliveryAddressResponseFormDto>>> GetListAsync(CancellationToken cancellationToken);

    Task<ResultDto<UserDeliveryAddressResponseFormDto>> UpdateAsync(Guid id, UserDeliveryAddressRequestFormDto dto, CancellationToken cancellationToken);
}
