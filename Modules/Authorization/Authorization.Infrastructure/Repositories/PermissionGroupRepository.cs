using Authorization.Infrastructure.Entities.PrermissionGroups;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Interfaces;

namespace Authorization.Infrastructure.Repositories;

public interface IPermissionGroupRepository : IBaseRepository<PermissionGroupEntity>, IUpdateRepository<PermissionGroupEntity>
{
}

internal class PermissionGroupRepository(AuthContext context) : BaseRepository<AuthContext, PermissionGroupEntity>(context), IPermissionGroupRepository
{
    public async Task<PermissionGroupEntity> UpdateAsync(Guid id, PermissionGroupEntity entity, CancellationToken cancellationToken)
    {
        var entityToUpdate = await _context.Set<PermissionGroupEntity>()
            .Include(x => x.AuthorizationPermissions)
            .Include(x => x.ProductPermissions)
            .Include(x => x.ShopPermissions)
            .Include(x => x.WarehousePermissions)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

        if (entityToUpdate == null)
            return null;

        entityToUpdate.Update(entity);
        entityToUpdate.Validate();

        await _context.SaveChangesAsync();

        return entityToUpdate;
    }
}