using Authorization.Core.Dtos.User;
using Shared.Core.Dtos;
using System.Threading.Tasks;
using System.Threading;
using System;
using Shared.Shared.Dtos;

namespace Authorization.Core.Interfaces.Services;

public interface IUserService
{
    Task<ResultDto<UserResponseFormDto>> CreateAsync(UserCreateRequestFormDto dto, CancellationToken cancellationToken);

    Task<ResultDto> DeleteAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto> DeleteOwnAccountAsync(CancellationToken cancellationToken);

    Task<ResultDto<UserResponseFormDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<UserBasicInfoFormDto>> GetOwnBasicInfoAsync(CancellationToken cancellationToken);

    Task<ResultDto<PageDto<UserListDto>>> GetPageListAsync(PaginationDto pagination, CancellationToken cancellationToken);

    Task<ResultDto<UserResponseFormDto>> UpdateAsync(Guid id, UserUpdateRequestFormDto dto, CancellationToken cancellationToken);

    Task<ResultDto<UserBasicInfoFormDto>> UpdateOwnBasicInfoAsync(UserBasicInfoFormDto dto, CancellationToken cancellationToken);

    Task<ResultDto> UpdateOwnPasswordAsync(UserPasswordFormDto dto, CancellationToken cancellationToken);
}
