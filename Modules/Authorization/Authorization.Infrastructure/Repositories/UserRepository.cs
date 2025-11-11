using Authorization.Infrastructure.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Exceptions;
using Shared.Infrastructure.Interfaces;

namespace Authorization.Infrastructure.Repositories;

public interface IUserRepository : IBaseRepository<UserEntity>, IUpdateRepository<UserEntity>
{
    Task<bool> AnyByEmailAsync(string email, CancellationToken cancellationToken);

    Task<UserEntity> GetByEmailAsync(string email, CancellationToken cancellationToken);

    Task<UserEntity> GetByTokenAsync(Guid token, CancellationToken cancellationToken);

    Task UpdatePasswordAsync(Guid id, string hashedPassword, CancellationToken cancellationToken);
}

internal class UserRepository(AuthContext context) : BaseRepository<AuthContext, UserEntity>(context), IUserRepository
{
    public Task<bool> AnyByEmailAsync(string email, CancellationToken cancellationToken)
        => AnyAsync(x => x.Email == email, cancellationToken);

    public Task<UserEntity> GetByEmailAsync(string email, CancellationToken cancellationToken)
        => GetByAsync(x => x.Email == email, cancellationToken);

    public Task<UserEntity> GetByTokenAsync(Guid token, CancellationToken cancellationToken)
        => GetByAsync(x => x.RefreshToken.Token == token, cancellationToken);

    public async Task<UserEntity> UpdateAsync(Guid id, UserEntity entity, CancellationToken cancellationToken)
    {
        var entitytoUpdate = await _context.Set<UserEntity>()
            .Include(x => x.UserPermissionGroups)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

        if (entitytoUpdate == null)
            return null;

        entitytoUpdate.Update(entity);
        await _context.SaveChangesAsync();

        return entitytoUpdate;
    }

    public async Task UpdatePasswordAsync(Guid id, string hashedPassword, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<UserEntity>().Where(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);

        if (entity == null)
            throw new RecordWasNotFoundException(id);

        entity.HashedPassword = hashedPassword;
        await _context.SaveChangesAsync();
    }
}