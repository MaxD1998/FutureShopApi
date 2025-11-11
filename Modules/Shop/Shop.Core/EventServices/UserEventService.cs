using Shared.Core.Bases;
using Shop.Core.Dtos.User;
using Shop.Infrastructure.Repositories;

namespace Shop.Core.EventServices;

public interface IUserEventService
{
    Task CreateOrUpdateAsync(UserEventDto dto, CancellationToken cancellationToken);

    Task DeleteByExternalIdAsync(Guid externalId, CancellationToken cancellationToken);
}

internal class UserEventService(IUserRepository userRepository) : IUserEventService
{
    private readonly IUserRepository _userRepository = userRepository;

    public Task CreateOrUpdateAsync(UserEventDto dto, CancellationToken cancellationToken)
    {
        var entity = dto.Map();
        return _userRepository.CreateOrUpdateForEventAsync(entity, cancellationToken);
    }

    public Task DeleteByExternalIdAsync(Guid externalId, CancellationToken cancellationToken)
        => _userRepository.DeleteByExternalIdAsync(externalId, cancellationToken);
}