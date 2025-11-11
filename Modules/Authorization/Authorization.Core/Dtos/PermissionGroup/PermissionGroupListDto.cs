using Authorization.Infrastructure.Entities.PrermissionGroups;
using System.Linq.Expressions;

namespace Authorization.Core.Dtos.PermissionGroup;

public class PermissionGroupListDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public static Expression<Func<PermissionGroupEntity, PermissionGroupListDto>> Map() => entity => new()
    {
        Id = entity.Id,
        Name = entity.Name,
    };
}