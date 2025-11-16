using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Interfaces;
using Shop.Infrastructure.Entities.Users;
using System.Linq.Expressions;

namespace Shop.Infrastructure.Repositories;

public interface IUserDeliveryAddressRepository : IBaseRepository<UserDeliveryAddressEntity>, IUpdateRepository<UserDeliveryAddressEntity>
{
    Task<TResult> GetByExternalIdAsync<TResult>(Guid externalId, Expression<Func<UserDeliveryAddressEntity, TResult>> map, CancellationToken cancellationToken);
}

internal class UserDeliveryAddressRepository(ShopContext context) : BaseRepository<ShopContext, UserDeliveryAddressEntity>(context), IUserDeliveryAddressRepository
{
    public Task<TResult> GetByExternalIdAsync<TResult>(Guid externalId, Expression<Func<UserDeliveryAddressEntity, TResult>> map, CancellationToken cancellationToken)
        => _context.Set<UserDeliveryAddressEntity>().AsNoTracking().Where(x => x.User.ExternalId == externalId).Select(map).FirstOrDefaultAsync();

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