using Authorization.Domain.Entities.PrermissionGroups;
using Shared.Core.Bases;
using System.Linq.Expressions;

namespace Authorization.Core.Dtos;

public class IdNameDto : BaseIdNameDto
{
    public static Expression<Func<PermissionGroupEntity, IdNameDto>> MapFromPermissionGroup() => entity => new IdNameDto
    {
        Id = entity.Id,
        Name = entity.Name
    };
}