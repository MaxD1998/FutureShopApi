using Authorization.Domain.Aggregates.Users;
using Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Interfaces;

namespace Authorization.Infrastructure.Repositories;

public interface IUserRepository : IBaseRepository<UserAggregate>, IUpdateRepository<UserAggregate>
{
    Task<bool> AnyByEmailAsync(string email, CancellationToken cancellationToken);

    Task<UserAggregate> GetByEmailAsync(string email, CancellationToken cancellationToken);

    Task<UserAggregate> GetByTokenAsync(Guid token, CancellationToken cancellationToken);

    Task RemoveRefreshTokenAsync(Guid id, CancellationToken cancellationToken);
}

public class UserRepository(AuthContext context) : BaseRepository<AuthContext, UserAggregate>(context), IUserRepository
{
    public Task<bool> AnyByEmailAsync(string email, CancellationToken cancellationToken)
        => AnyAsync(x => x.Email == email, cancellationToken);

    public Task<UserAggregate> GetByEmailAsync(string email, CancellationToken cancellationToken)
        => GetByAsync(x => x.Email == email, cancellationToken);

    public Task<UserAggregate> GetByTokenAsync(Guid token, CancellationToken cancellationToken)
        => GetByAsync(x => x.RefreshToken.Token == token, cancellationToken);

    public async Task RemoveRefreshTokenAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await _context.Set<UserAggregate>()
            .Include(x => x.RefreshToken)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

        if (user == null)
            return;

        user.RemoveRefreshToken();

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<UserAggregate> UpdateAsync(Guid id, UserAggregate entity, CancellationToken cancellationToken)
    {
        var entityToUpdate = await _context.Set<UserAggregate>()
            .Include(x => x.RefreshToken)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (entity == null)
            return null;

        entityToUpdate.Update(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entityToUpdate;
    }
}