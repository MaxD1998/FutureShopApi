using Shared.Core.Interfaces;
using Shop.Domain.Entities.Users;

namespace Shop.Core.Interfaces.Repositories;

public interface IUserCompanyDetailsRepository : IBaseRepository<UserCompanyDetailsEntity>, IUpdateRepository<UserCompanyDetailsEntity>
{
    Task<bool> AnyIsDefaultByUserExternalIdAsync(Guid userExternalId, CancellationToken cancellationToken);

    Task ClearIsDefaultByUserExternalIdAsync(Guid userExternalId, CancellationToken cancellationToken);
}