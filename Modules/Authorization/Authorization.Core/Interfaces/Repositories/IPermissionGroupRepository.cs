using Authorization.Domain.Entities.PrermissionGroups;
using Shared.Core.Interfaces;

namespace Authorization.Core.Interfaces.Repositories;

public interface IPermissionGroupRepository : IBaseRepository<PermissionGroupEntity>, IUpdateRepository<PermissionGroupEntity>
{
}