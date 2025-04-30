using Authorization.Domain.Entities;
using Shared.Infrastructure.Bases;

namespace Authorization.Inrfrastructure.Repositories;

public interface IUserRepository
{
    Task<bool> AnyByEmailAsync(string email, CancellationToken cancellationToken);

    Task<UserEntity> CreateAsync(UserEntity entity, CancellationToken cancellationToken);

    Task<UserEntity> GetByEmailAsync(string email, CancellationToken cancellationToken);

    Task<UserEntity> GetByTokenAsync(Guid token, CancellationToken cancellationToken);
}

public class UserRepository(AuthContext context) : BaseRepository<AuthContext>(context), IUserRepository
{
    public Task<bool> AnyByEmailAsync(string email, CancellationToken cancellationToken)
        => AnyAsync<UserEntity>(x => x.Email == email, cancellationToken);

    public Task<UserEntity> CreateAsync(UserEntity entity, CancellationToken cancellationToken)
        => base.CreateAsync(entity, cancellationToken);

    public Task<UserEntity> GetByEmailAsync(string email, CancellationToken cancellationToken)
        => GetByAsync<UserEntity>(x => x.Email == email, cancellationToken);

    public Task<UserEntity> GetByTokenAsync(Guid token, CancellationToken cancellationToken)
        => GetByAsync<UserEntity>(x => x.RefreshToken.Token == token, cancellationToken);
}