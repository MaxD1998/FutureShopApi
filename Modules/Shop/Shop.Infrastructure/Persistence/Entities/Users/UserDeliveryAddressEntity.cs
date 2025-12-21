using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Interfaces;

namespace Shop.Infrastructure.Persistence.Entities.Users;

public class UserDeliveryAddressEntity : BaseEntity, IUpdate<UserDeliveryAddressEntity>, IEntityValidation
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

    public Guid UserId { get; set; }

    #region Related Data

    public UserEntity User { get; set; }

    #endregion Related Data

    #region Methods

    public void Update(UserDeliveryAddressEntity entity)
    {
        ApartamentNumber = entity.ApartamentNumber;
        City = entity.City;
        Email = entity.Email;
        FirstName = entity.FirstName;
        HouseNumber = entity.HouseNumber;
        IsDefault = entity.IsDefault;
        LastName = entity.LastName;
        PhoneNumber = entity.PhoneNumber;
        PostalCode = entity.PostalCode;
        Street = entity.Street;
    }

    public void Validate()
    {
    }

    #endregion Methods
}