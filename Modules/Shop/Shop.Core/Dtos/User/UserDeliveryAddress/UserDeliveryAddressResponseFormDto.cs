using Shop.Domain.Entities.Users;
using System.Linq.Expressions;

namespace Shop.Core.Dtos.User.UserDeliveryAddress;

public class UserDeliveryAddressResponseFormDto : UserDeliveryAddressRequestFormDto
{
    public Guid Id { get; set; }

    public static Expression<Func<UserDeliveryAddressEntity, UserDeliveryAddressResponseFormDto>> Map() => entity => new()
    {
        Id = entity.Id,
        ApartamentNumber = entity.ApartamentNumber,
        City = entity.City,
        Email = entity.Email,
        FirstName = entity.FirstName,
        HouseNumber = entity.HouseNumber,
        IsDefault = entity.IsDefault,
        LastName = entity.LastName,
        PhoneNumber = entity.PhoneNumber,
        PostalCode = entity.PostalCode,
        Street = entity.Street,
    };
}