using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Constants;
using Shared.Infrastructure.Exceptions;
using Shared.Infrastructure.Interfaces;

namespace Shop.Infrastructure.Entities.Users;

public class UserPhoneNumberEntity : BaseEntity, IUpdate<UserPhoneNumberEntity>, IEntityValidation
{
    public string PhoneNumber { get; set; }

    public Guid UserId { get; set; }

    #region Methods

    public void Update(UserPhoneNumberEntity entity)
    {
        entity.PhoneNumber = PhoneNumber;
    }

    public void Validate()
    {
        ValidatePhoneNumber();
    }

    private void ValidatePhoneNumber()
    {
        var phoneNumber = PhoneNumber ?? string.Empty;
        var length = StringLengthConst.ShortString;

        if (phoneNumber.Length > length)
            throw new PropertyWasTooLongException(nameof(PhoneNumber), length);
    }

    #endregion Methods
}