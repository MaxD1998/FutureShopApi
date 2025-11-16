using Shop.Infrastructure.Entities.Users;
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
        HouseNumber = entity.HouseNumber,
        IsDefault = entity.IsDefault,
        PhoneNumber = entity.PhoneNumber,
        PostalCode = entity.PostalCode,
        Street = entity.Street,
    };
}