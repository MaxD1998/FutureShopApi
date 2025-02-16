using Shared.Domain.Bases;
using Shared.Domain.Enums;

namespace Authorization.Domain.Entities;

public class UserModuleEntity : BaseEntity
{
    public bool CanDelete { get; set; }

    public bool CanEdit { get; set; }

    public ModuleType Type { get; set; }

    public Guid UserId { get; set; }

    #region Related Data

    public UserEntity User { get; set; }

    #endregion Related Data
}