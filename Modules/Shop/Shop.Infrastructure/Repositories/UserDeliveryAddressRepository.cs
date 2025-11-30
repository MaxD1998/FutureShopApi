using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Interfaces;
using Shop.Infrastructure.Entities.Users;

namespace Shop.Infrastructure.Repositories;

public interface IUserDeliveryAddressRepository : IBaseRepository<UserDeliveryAddressEntity>, IUpdateRepository<UserDeliveryAddressEntity>
{
    Task<bool> AnyIsDefaultByUserExternalIdAsync(Guid userExternalId, CancellationToken cancellationToken);

    Task ClearIsDefaultByUserExternalIdAsync(Guid userExternalId, CancellationToken cancellationToken);
}

internal class UserDeliveryAddressRepository(ShopContext context) : BaseRepository<ShopContext, UserDeliveryAddressEntity>(context), IUserDeliveryAddressRepository
{
    public Task<bool> AnyIsDefaultByUserExternalIdAsync(Guid userExternalId, CancellationToken cancellationToken)
        => _context.Set<UserDeliveryAddressEntity>().AnyAsync(x => x.User.ExternalId == userExternalId && x.IsDefault, cancellationToken);

    public async Task ClearIsDefaultByUserExternalIdAsync(Guid userExternalId, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<UserDeliveryAddressEntity>().Where(x => x.User.ExternalId == userExternalId && x.IsDefault).FirstOrDefaultAsync(cancellationToken);

        entity.IsDefault = false;

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<UserDeliveryAddressEntity> UpdateAsync(Guid id, UserDeliveryAddressEntity entity, CancellationToken cancellationToken)
    {
        var entityToUpdate = await _context.Set<UserDeliveryAddressEntity>().Where(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);

        if (entityToUpdate == null)
            return null;

        entityToUpdate.Update(entity);
        entityToUpdate.Validate();

        await _context.SaveChangesAsync(cancellationToken);

        return entityToUpdate;
    }
}