using Authorization.Infrastructure.Exceptions.Users;
using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Constants;
using Shared.Infrastructure.Enums;
using Shared.Infrastructure.Exceptions;
using Shared.Infrastructure.Extensions;
using Shared.Infrastructure.Interfaces;

namespace Authorization.Infrastructure.Entities.Users;

public class UserEntity : BaseEntity, IUpdate<UserEntity>, IEntityValidation
{
    public DateOnly DateOfBirth { get; set; }

    public string Email { get; set; }

    public string FirstName { get; set; }

    public string HashedPassword { get; set; }

    public string LastName { get; set; }

    public string PhoneNumber { get; set; }

    public UserType Type { get; set; } = UserType.Customer;

    #region Related Data

    public RefreshTokenEntity RefreshToken { get; set; }

    public ICollection<UserPermissionGroupEntity> UserPermissionGroups { get; set; } = [];

    #endregion Related Data

    #region Methods

    public void Update(UserEntity entity)
    {
        DateOfBirth = entity.DateOfBirth;
        Email = entity.Email;
        FirstName = entity.FirstName;
        LastName = entity.LastName;
        PhoneNumber = entity.PhoneNumber;
        UserPermissionGroups.UpdateEntities(entity.UserPermissionGroups);
    }

    public void Validate()
    {
        ValidateEmail();
        ValidateFirstName();
        ValidateHashedPassword();
        ValidateLastName();
        ValidatePhoneNumber();
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email.Trim();
        }
        catch
        {
            return false;
        }
    }

    private void ValidateEmail()
    {
        if (string.IsNullOrWhiteSpace(Email))
            throw new PropertyWasEmptyException(nameof(Email));

        var length = StringLengthConst.LongString;

        if (Email.Length > length)
            throw new PropertyWasTooLongException(nameof(Email), length);

        if (!IsValidEmail(Email))
            throw new UserInvalidEmailFormatException();
    }

    private void ValidateFirstName()
    {
        if (string.IsNullOrWhiteSpace(FirstName))
            throw new PropertyWasEmptyException(nameof(FirstName));

        var length = StringLengthConst.LongString;

        if (Email.Length > length)
            throw new PropertyWasTooLongException(nameof(FirstName), length);
    }

    private void ValidateHashedPassword()
    {
        if (string.IsNullOrWhiteSpace(HashedPassword))
            throw new PropertyWasEmptyException(nameof(HashedPassword));
    }

    private void ValidateLastName()
    {
        if (string.IsNullOrWhiteSpace(Email))
            throw new PropertyWasEmptyException(nameof(LastName));

        var length = StringLengthConst.LongString;

        if (LastName.Length > length)
            throw new PropertyWasTooLongException(nameof(LastName), length);
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