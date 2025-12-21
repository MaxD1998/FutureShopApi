using Authorization.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Bases;
using Authorization.Core.Interfaces.Repositories;

namespace Authorization.Infrastructure.Repositories;

internal class RefreshTokenRepository(AuthContext context) : BaseRepository<AuthContext, RefreshTokenEntity>(context), IRefreshTokenRepository
{
    public async Task<RefreshTokenEntity> CreateOrUpdateByUserIdAsync(RefreshTokenEntity entityToUpdate, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<RefreshTokenEntity>().FirstOrDefaultAsync(x => x.UserId == entityToUpdate.UserId, cancellationToken);

        entity ??= new RefreshTokenEntity();
        entity.Update(entityToUpdate);

        if (entity.Id == Guid.Empty)
            await _context.Set<RefreshTokenEntity>().AddAsync(entity, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public Task DeleteByUserId(Guid userId, CancellationToken cancellationToken)
        => DeleteByAsync(x => x.UserId == userId, cancellationToken);
}