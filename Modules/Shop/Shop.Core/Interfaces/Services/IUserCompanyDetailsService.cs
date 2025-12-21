using Shared.Core.Dtos;
using Shared.Core.Errors;
using Shop.Core.Dtos.User.UserCompanyDetails;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Core.Interfaces.Services;

public interface IUserCompanyDetailsService
{
    Task<ResultDto<UserCompanyDetailsResponseFormDto>> CreateAsync(UserCompanyDetailsRequestFormDto dto, CancellationToken cancellationToken);

    Task<ResultDto> DeleteByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<List<UserCompanyDetailsResponseFormDto>>> GetListAsync(CancellationToken cancellationToken);

    Task<ResultDto<UserCompanyDetailsResponseFormDto>> UpdateAsync(Guid id, UserCompanyDetailsRequestFormDto dto, CancellationToken cancellationToken);
}
