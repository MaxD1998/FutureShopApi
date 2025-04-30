using Authorization.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Bases;

namespace Authorization.Inrfrastructure.Repositories;

public interface IRefreshTokenRepository
{
    Task<RefreshTokenEntity> CreateOrUpdateByUserIdAsync(RefreshTokenEntity entityToUpdate, CancellationToken cancellationToken);

    Task DeleteByUserId(Guid userId, CancellationToken cancellationToken);
}

public class RefreshTokenRepository(AuthContext context) : BaseRepository<AuthContext>(context), IRefreshTokenRepository
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
        => DeleteByIdAsync<RefreshTokenEntity>(x => x.UserId == userId, cancellationToken);
}