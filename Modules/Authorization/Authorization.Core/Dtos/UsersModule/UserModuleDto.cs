using Authorization.Domain.Aggregates.Users.Entities;
using Shared.Domain.Enums;

namespace Authorization.Core.Dtos.UsersModule;

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