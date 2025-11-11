using Authorization.Core.Dtos;
using Authorization.Core.Dtos.PermissionGroup;
using Authorization.Infrastructure.Repositories;
using Shared.Core.Dtos;
using Shared.Infrastructure.Extensions;
using Shared.Shared.Dtos;

namespace Authorization.Core.Services;

public interface IPermissionGroupService
{
    //101473Fital MichalskaF
    Task<ResultDto<PermissionGroupResponseFormDto>> CreateAsync(PermissionGroupRequestFormDto dto, CancellationToken cancellationToken);

    Task<ResultDto> DeleteByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<PermissionGroupResponseFormDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<List<IdNameDto>>> GetListIdNameAsync(List<Guid> excludedIds, CancellationToken cancellationToken);

    Task<ResultDto<PageDto<PermissionGroupListDto>>> GetPageListAsync(PaginationDto pagination, CancellationToken cancellationToken);

    Task<ResultDto<PermissionGroupResponseFormDto>> UpdateAsync(Guid id, PermissionGroupRequestFormDto dto, CancellationToken cancellationToken);
}

internal class PermissionGroupService(IPermissionGroupRepository permissionGroupRepository) : IPermissionGroupService
{
    private readonly IPermissionGroupRepository _permissionGroupRepository = permissionGroupRepository;

    public async Task<ResultDto<PermissionGroupResponseFormDto>> CreateAsync(PermissionGroupRequestFormDto dto, CancellationToken cancellationToken)
    {
        var entity = await _permissionGroupRepository.CreateAsync(dto.ToEntity(), cancellationToken);
        var result = await _permissionGroupRepository.GetByIdAsync(entity.Id, PermissionGroupResponseFormDto.Map(), cancellationToken);

        return ResultDto.Success(result);
    }

    public async Task<ResultDto> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _permissionGroupRepository.DeleteByIdAsync(id, cancellationToken);

        return ResultDto.Success();
    }

    public async Task<ResultDto<PermissionGroupResponseFormDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _permissionGroupRepository.GetByIdAsync(id, PermissionGroupResponseFormDto.Map(), cancellationToken);

        return ResultDto.Success(result);
    }

    public async Task<ResultDto<List<IdNameDto>>> GetListIdNameAsync(List<Guid> excludedIds, CancellationToken cancellationToken)
    {
        var result = await _permissionGroupRepository.GetListAsync(x => !excludedIds.Contains(x.Id), IdNameDto.MapFromPermissionGroup(), cancellationToken);

        return ResultDto.Success(result);
    }

    public async Task<ResultDto<PageDto<PermissionGroupListDto>>> GetPageListAsync(PaginationDto pagination, CancellationToken cancellationToken)
    {
        var results = await _permissionGroupRepository.GetPageAsync(pagination, PermissionGroupListDto.Map(), cancellationToken);

        return ResultDto.Success(results);
    }

    public async Task<ResultDto<PermissionGroupResponseFormDto>> UpdateAsync(Guid id, PermissionGroupRequestFormDto dto, CancellationToken cancellationToken)
    {
        var entity = await _permissionGroupRepository.UpdateAsync(id, dto.ToEntity(), cancellationToken);
        var result = await _permissionGroupRepository.GetByIdAsync(entity.Id, PermissionGroupResponseFormDto.Map(), cancellationToken);

        return ResultDto.Success(result);
    }
}