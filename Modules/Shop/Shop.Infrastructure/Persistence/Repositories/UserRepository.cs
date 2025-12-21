using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Bases;
using Shop.Infrastructure.Persistence;
using Shop.Domain.Entities.Users;

namespace Shop.Infrastructure.Persistence.Repositories;

public interface IUserRepository : IBaseRepository<UserEntity>
{
    Task CreateOrUpdateForEventAsync(UserEntity eventEntity, CancellationToken cancellationToken);

    Task DeleteByExternalIdAsync(Guid externalId, CancellationToken cancellationToken);

    Task<Guid> GetIdByExternalIdAsync(Guid externalId, CancellationToken cancellationToken);
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

    public Task<Guid> GetIdByExternalIdAsync(Guid externalId, CancellationToken cancellationToken)
        => _context.Set<UserEntity>().Where(x => x.ExternalId == externalId).Select(x => x.Id).FirstOrDefaultAsync(cancellationToken);
}