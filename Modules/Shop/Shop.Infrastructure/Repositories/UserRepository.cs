using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Bases;
using Shop.Infrastructure.Entities.Users;

namespace Shop.Infrastructure.Repositories;

public interface IUserRepository : IBaseRepository<UserEntity>
{
    Task CreateOrUpdateForEventAsync(UserEntity eventEntity, CancellationToken cancellationToken);

    Task DeleteByExternalIdAsync(Guid externalId, CancellationToken cancellationToken);
}

internal class UserRepository(ShopContext context) : BaseRepository<ShopContext, UserEntity>(context), IUserRepository
{
    public async Task CreateOrUpdateForEventAsync(UserEntity eventEntity, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<UserEntity>()
            .FirstOrDefaultAsync(x => x.ExternalId == eventEntity.ExternalId, cancellationToken);

        if (entity is null)
            await _context.Set<UserEntity>().AddAsync(eventEntity, cancellationToken);
        else
            entity.UpdateEvent(eventEntity);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public Task DeleteByExternalIdAsync(Guid externalId, CancellationToken cancellationToken)
        => _context.Set<UserEntity>().Where(x => x.ExternalId == externalId).ExecuteDeleteAsync(cancellationToken);
}