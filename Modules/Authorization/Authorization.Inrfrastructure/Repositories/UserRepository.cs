using Authorization.Domain.Aggregates.Users;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Interfaces;

namespace Authorization.Inrfrastructure.Repositories;

public interface IUserRepository : IBaseRepository<User>, IUpdateRepository<User>
{
    Task<bool> AnyByEmailAsync(string email, CancellationToken cancellationToken);

    Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken);

    Task<User> GetByTokenAsync(Guid token, CancellationToken cancellationToken);

    Task RemoveRefreshTokenAsync(Guid id, CancellationToken cancellationToken);
}

public class UserRepository(AuthContext context) : BaseRepository<AuthContext, User>(context), IUserRepository
{
    public Task<bool> AnyByEmailAsync(string email, CancellationToken cancellationToken)
        => AnyAsync(x => x.Email == email, cancellationToken);

    public Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken)
        => GetByAsync(x => x.Email == email, cancellationToken);

    public Task<User> GetByTokenAsync(Guid token, CancellationToken cancellationToken)
        => GetByAsync(x => x.RefreshToken.Token == token, cancellationToken);

    public async Task RemoveRefreshTokenAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await _context.Set<User>()
            .Include(x => x.RefreshToken)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

        if (user == null)
            return;

        user.RemoveRefreshToken();

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<User> UpdateAsync(Guid id, User entity, CancellationToken cancellationToken)
    {
        var entityToUpdate = await _context.Set<User>()
            .Include(x => x.RefreshToken)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (entity == null)
            return null;

        entityToUpdate.Update(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entityToUpdate;
    }
}