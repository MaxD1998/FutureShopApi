using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Interfaces;
using Shop.Infrastructure.Persistence;
using Shop.Domain.Entities.Users;

namespace Shop.Infrastructure.Persistence.Repositories;

public interface IUserCompanyDetailsRepository : IBaseRepository<UserCompanyDetailsEntity>, IUpdateRepository<UserCompanyDetailsEntity>
{
    Task<bool> AnyIsDefaultByUserExternalIdAsync(Guid userExternalId, CancellationToken cancellationToken);

    Task ClearIsDefaultByUserExternalIdAsync(Guid userExternalId, CancellationToken cancellationToken);
}

internal class UserCompanyDetailsRepository(ShopContext context) : BaseRepository<ShopContext, UserCompanyDetailsEntity>(context), IUserCompanyDetailsRepository
{
    public Task<bool> AnyIsDefaultByUserExternalIdAsync(Guid userExternalId, CancellationToken cancellationToken)
        => _context.Set<UserCompanyDetailsEntity>().AnyAsync(x => x.User.ExternalId == userExternalId && x.IsDefault, cancellationToken);

    public async Task ClearIsDefaultByUserExternalIdAsync(Guid userExternalId, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<UserCompanyDetailsEntity>().Where(x => x.User.ExternalId == userExternalId && x.IsDefault).FirstOrDefaultAsync(cancellationToken);

        entity.IsDefault = false;

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<UserCompanyDetailsEntity> UpdateAsync(Guid id, UserCompanyDetailsEntity entity, CancellationToken cancellationToken)
    {
        var entityToUpdate = await _context.Set<UserCompanyDetailsEntity>().Where(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);

        if (entityToUpdate == null)
            return null;

        entityToUpdate.Update(entity);
        entityToUpdate.Validate();

        await _context.SaveChangesAsync(cancellationToken);

        return entityToUpdate;
    }
}