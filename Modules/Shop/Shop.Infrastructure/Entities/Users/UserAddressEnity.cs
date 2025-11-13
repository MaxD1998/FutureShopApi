using Shared.Infrastructure.Bases;

namespace Shop.Infrastructure.Entities.Users;

public class UserAddressEnity : BaseEntity
{
    public string City { get; set; }

    public string PostalCode { get; set; }

    public string Street { get; set; }

    public Guid UserId { get; set; }
}