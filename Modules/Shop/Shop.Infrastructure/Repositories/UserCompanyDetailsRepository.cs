using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Interfaces;
using Shop.Infrastructure.Entities.Users;
using System.Linq.Expressions;

namespace Shop.Infrastructure.Repositories;

public interface IUserCompanyDetailsRepository : IBaseRepository<UserCompanyDetailsEntity>, IUpdateRepository<UserCompanyDetailsEntity>
{
    Task<TResult> GetByExternalIdAsync<TResult>(Guid externalId, Expression<Func<UserCompanyDetailsEntity, TResult>> map, CancellationToken cancellationToken);
}

internal class UserCompanyDetailsRepository(ShopContext context) : BaseRepository<ShopContext, UserCompanyDetailsEntity>(context), IUserCompanyDetailsRepository
{
    public Task<TResult> GetByExternalIdAsync<TResult>(Guid externalId, Expression<Func<UserCompanyDetailsEntity, TResult>> map, CancellationToken cancellationToken)
        => _context.Set<UserCompanyDetailsEntity>().AsNoTracking().Where(x => x.User.ExternalId == externalId).Select(map).FirstOrDefaultAsync();

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