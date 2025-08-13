using Authorization.Infrastructure.Entities;
using Shared.Infrastructure.Enums;

namespace Authorization.Core.Dtos.UserModule;

public class UserModuleDto
{
    public UserModuleDto(UserModuleEntity entity)
    {
        CanDelete = entity.CanDelete;
        CanEdit = entity.CanEdit;
        Type = entity.Type;
    }

    public bool CanDelete { get; set; }

    public bool CanEdit { get; set; }

    public ModuleType Type { get; set; }
}