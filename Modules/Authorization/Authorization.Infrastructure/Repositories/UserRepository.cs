using Authorization.Infrastructure.Entities.Users;
using Shared.Infrastructure.Bases;

namespace Authorization.Infrastructure.Repositories;

public interface IUserRepository : IBaseRepository<UserEntity>
{
    Task<bool> AnyByEmailAsync(string email, CancellationToken cancellationToken);

    Task<UserEntity> GetByEmailAsync(string email, CancellationToken cancellationToken);

    Task<UserEntity> GetByTokenAsync(Guid token, CancellationToken cancellationToken);
}

internal class UserRepository(AuthContext context) : BaseRepository<AuthContext, UserEntity>(context), IUserRepository
{
    public Task<bool> AnyByEmailAsync(string email, CancellationToken cancellationToken)
        => AnyAsync(x => x.Email == email, cancellationToken);

    public Task<UserEntity> GetByEmailAsync(string email, CancellationToken cancellationToken)
        => GetByAsync(x => x.Email == email, cancellationToken);

    public Task<UserEntity> GetByTokenAsync(Guid token, CancellationToken cancellationToken)
        => GetByAsync(x => x.RefreshToken.Token == token, cancellationToken);
}