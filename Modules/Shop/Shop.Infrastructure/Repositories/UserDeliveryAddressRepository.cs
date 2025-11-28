using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Interfaces;
using Shop.Infrastructure.Entities.Users;

namespace Shop.Infrastructure.Repositories;

public interface IUserDeliveryAddressRepository : IBaseRepository<UserDeliveryAddressEntity>, IUpdateRepository<UserDeliveryAddressEntity>
{
}

internal class UserDeliveryAddressRepository(ShopContext context) : BaseRepository<ShopContext, UserDeliveryAddressEntity>(context), IUserDeliveryAddressRepository
{
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