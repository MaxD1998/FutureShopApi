using Shared.Core.Interfaces;
using Shop.Domain.Entities.Users;

namespace Shop.Core.Interfaces.Repositories;

public interface IUserRepository : IBaseRepository<UserEntity>
{
    Task CreateOrUpdateForEventAsync(UserEntity eventEntity, CancellationToken cancellationToken);

    Task DeleteByExternalIdAsync(Guid externalId, CancellationToken cancellationToken);

    Task<Guid> GetIdByExternalIdAsync(Guid externalId, CancellationToken cancellationToken);
}