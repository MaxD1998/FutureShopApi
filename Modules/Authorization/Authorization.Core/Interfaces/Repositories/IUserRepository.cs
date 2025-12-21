using Authorization.Domain.Entities.Users;
using Shared.Core.Interfaces;

namespace Authorization.Core.Interfaces.Repositories;

public interface IUserRepository : IBaseRepository<UserEntity>, IUpdateRepository<UserEntity>
{
    Task<bool> AnyByEmailAsync(string email, CancellationToken cancellationToken);

    Task<UserEntity> GetByEmailAsync(string email, CancellationToken cancellationToken);

    Task<UserEntity> GetByTokenAsync(Guid token, CancellationToken cancellationToken);

    Task<UserEntity> UpdateBasicInfoAsync(Guid id, UserEntity entity, CancellationToken cancellationToken);

    Task UpdatePasswordAsync(Guid id, string hashedPassword, CancellationToken cancellationToken);
}