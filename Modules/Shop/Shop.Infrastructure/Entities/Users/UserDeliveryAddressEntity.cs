using Shared.Infrastructure.Bases;

namespace Shop.Infrastructure.Entities.Users;

public class UserDeliveryAddressEntity : BaseEntity
{
    public string ApartamentNumber { get; set; }

    public string City { get; set; }

    public string Email { get; set; }

    public string HouseNumber { get; set; }

    public bool IsDefault { get; set; }

    public string PhoneNumber { get; set; }

    public string PostalCode { get; set; }

    public string Street { get; set; }

    public Guid UserId { get; set; }

    #region Related Data

    public UserEntity User { get; set; }

    #endregion Related Data
}