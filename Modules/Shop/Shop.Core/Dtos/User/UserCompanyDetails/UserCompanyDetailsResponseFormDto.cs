using Shop.Infrastructure.Entities.Users;
using System.Linq.Expressions;

namespace Shop.Core.Dtos.User.UserCompanyDetails;

public class UserCompanyDetailsResponseFormDto : UserCompanyDetailsRequestFormDto
{
    public Guid Id { get; set; }

    public static Expression<Func<UserCompanyDetailsEntity, UserCompanyDetailsResponseFormDto>> Map() => entity => new()
    {
        Id = entity.Id,
        ApartamentNumber = entity.ApartamentNumber,
        City = entity.City,
        CompanyIdentifierNumber = entity.CompanyIdentifierNumber,
        HouseNumber = entity.HouseNumber,
        IsDefault = entity.IsDefault,
        PostalCode = entity.PostalCode,
        Street = entity.Street,
        Type = entity.Type,
    };
}