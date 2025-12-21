using Authorization.Domain.Entities.Users;
using Shared.Core.Interfaces;

namespace Authorization.Core.Interfaces.Repositories;

public interface IRefreshTokenRepository : IBaseRepository<RefreshTokenEntity>
{
    Task<RefreshTokenEntity> CreateOrUpdateByUserIdAsync(RefreshTokenEntity entityToUpdate, CancellationToken cancellationToken);

    Task DeleteByUserId(Guid userId, CancellationToken cancellationToken);
}