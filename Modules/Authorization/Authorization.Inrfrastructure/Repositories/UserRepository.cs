using Authorization.Domain.Aggregates.Users;
using Shared.Infrastructure.Bases;

namespace Authorization.Inrfrastructure.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<bool> AnyByEmailAsync(string email, CancellationToken cancellationToken);

    Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken);

    Task<User> GetByTokenAsync(Guid token, CancellationToken cancellationToken);
}

public class UserRepository(AuthContext context) : BaseRepository<AuthContext, User>(context), IUserRepository
{
    public Task<bool> AnyByEmailAsync(string email, CancellationToken cancellationToken)
        => AnyAsync(x => x.Email == email, cancellationToken);

    public Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken)
        => GetByAsync(x => x.Email == email, cancellationToken);

    public Task<User> GetByTokenAsync(Guid token, CancellationToken cancellationToken)
        => GetByAsync(x => x.RefreshToken.Token == token, cancellationToken);
}