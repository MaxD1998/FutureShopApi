using Authorization.Core.Dtos.PermissionGroup;
using Shared.Core.Dtos;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Collections.Generic;
using Shared.Shared.Dtos;
using Authorization.Core.Dtos;

namespace Authorization.Core.Interfaces.Services;

public interface IPermissionGroupService
{
    Task<ResultDto<PermissionGroupResponseFormDto>> CreateAsync(PermissionGroupRequestFormDto dto, CancellationToken cancellationToken);

    Task<ResultDto> DeleteByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<PermissionGroupResponseFormDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<List<IdNameDto>>> GetListIdNameAsync(List<Guid> excludedIds, CancellationToken cancellationToken);

    Task<ResultDto<PageDto<PermissionGroupListDto>>> GetPageListAsync(PaginationDto pagination, CancellationToken cancellationToken);

    Task<ResultDto<PermissionGroupResponseFormDto>> UpdateAsync(Guid id, PermissionGroupRequestFormDto dto, CancellationToken cancellationToken);
}