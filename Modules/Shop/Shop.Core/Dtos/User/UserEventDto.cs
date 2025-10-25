using Shop.Infrastructure.Entities.Users;

namespace Shop.Core.Dtos.User;

public class UserEventDto
{
    public string FirstName { get; set; }

    public Guid Id { get; set; }

    public string LastName { get; set; }

    public UserEntity Map() => new()
    {
        ExternalId = Id,
        FirstName = FirstName,
        LastName = LastName,
    };
}