using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Interfaces;
using Shop.Infrastructure.Enums;

namespace Shop.Infrastructure.Entities.Users;

public class UserCompanyDetailsEntity : BaseEntity, IUpdate<UserCompanyDetailsEntity>, IEntityValidation
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

    public Guid UserId { get; set; }

    #region Related Data

    public UserEntity User { get; set; }

    #endregion Related Data

    #region Methods

    public void Update(UserCompanyDetailsEntity entity)
    {
        ApartamentNumber = entity.ApartamentNumber;
        City = entity.City;
        CompanyIdentifierNumber = entity.CompanyIdentifierNumber;
        CompanyName = entity.CompanyName;
        HouseNumber = entity.HouseNumber;
        IsDefault = entity.IsDefault;
        PostalCode = entity.PostalCode;
        Street = entity.Street;
        Type = entity.Type;
    }

    public void Validate()
    {
    }

    #endregion Methods
}