using Shared.Domain.Bases;
using Shared.Domain.Enums;

namespace Authorization.Domain.Aggregates.Users.Entities;

public class UserModuleEntity : BaseEntity
{
    public bool CanDelete { get; set; }

    public bool CanEdit { get; set; }

    public ModuleType Type { get; set; }

    public Guid UserId { get; set; }

    #region Related Data

    public User User { get; set; }

    #endregion Related Data
}