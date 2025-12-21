using Shop.Infrastructure.Persistence.Entities.Users;

namespace Shop.Core.Dtos.User.UserDeliveryAddress;

public class UserDeliveryAddressRequestFormDto
{
    public string ApartamentNumber { get; set; }

    public string City { get; set; }

    public string Email { get; set; }

    public string FirstName { get; set; }

    public string HouseNumber { get; set; }

    public bool IsDefault { get; set; }

    public string LastName { get; set; }

    public string PhoneNumber { get; set; }

    public string PostalCode { get; set; }

    public string Street { get; set; }

    public UserDeliveryAddressEntity ToEntity() => new()
    {
        ApartamentNumber = ApartamentNumber,
        City = City,
        Email = Email,
        FirstName = FirstName,
        HouseNumber = HouseNumber,
        IsDefault = IsDefault,
        LastName = LastName,
        PhoneNumber = PhoneNumber,
        PostalCode = PostalCode,
        Street = Street,
    };
}