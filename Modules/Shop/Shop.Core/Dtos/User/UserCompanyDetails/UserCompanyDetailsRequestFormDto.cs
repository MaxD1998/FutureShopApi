using Shop.Infrastructure.Persistence.Entities.Users;
using Shop.Infrastructure.Persistence.Enums;

namespace Shop.Core.Dtos.User.UserCompanyDetails;

public class UserCompanyDetailsRequestFormDto
{
    public string ApartamentNumber { get; set; }

    public string City { get; set; }

    public string CompanyIdentifierNumber { get; set; }

    public string CompanyName { get; set; }

    public string HouseNumber { get; set; }

    public bool IsDefault { get; set; }

    public string PostalCode { get; set; }

    public string Street { get; set; }

    public CompanyIdentifierNumberType Type { get; set; }

    public UserCompanyDetailsEntity ToEntity() => new()
    {
        ApartamentNumber = ApartamentNumber,
        City = City,
        CompanyIdentifierNumber = CompanyIdentifierNumber,
        CompanyName = CompanyName,
        HouseNumber = HouseNumber,
        IsDefault = IsDefault,
        PostalCode = PostalCode,
        Street = Street,
        Type = Type,
    };
}