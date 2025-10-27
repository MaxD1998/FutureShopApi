using Authorization.Infrastructure.Entities.Permissions;
using Shared.Infrastructure.Enums;

namespace Authorization.Core.Dtos.UserModule;

public class UserModuleDto
{
    public UserModuleDto(PermissionModuleEntity entity)
    {
        Type = entity.Type;
    }

    public ModuleType Type { get; set; }
}