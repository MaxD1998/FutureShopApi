using Authorization.Domain.Aggregates.Users.Entities;
using Authorization.Domain.Aggregates.Users.Entities.RefreshTokens;
using Authorization.Domain.Aggregates.Users.Exceptions;
using Authorization.Domain.Exceptions;
using Shared.Domain.Bases;
using Shared.Domain.Constants;
using Shared.Domain.Enums;
using Shared.Domain.Interfaces;
using Shared.Shared.Constants;

namespace Authorization.Domain.Aggregates.Users;

public class User : BaseEntity, IUpdate<User>
{
    public User(DateOnly dateOfBirth, string email, string firstName, string hashedPassword, string lastName)
    {
        SetDateOfBirth(dateOfBirth);
        SetEmail(email);
        SetFirstName(firstName);
        SetHashedPassword(hashedPassword);
        SetLastName(lastName);
    }

    public User(DateOnly dateOfBirth, string email, string firstName, string hashedPassword, string lastName, string phoneNumber)
    {
        SetDateOfBirth(dateOfBirth);
        SetEmail(email);
        SetFirstName(firstName);
        SetHashedPassword(hashedPassword);
        SetLastName(lastName);
        SetPhoneNumber(phoneNumber);
    }

    private User()
    {
    }

    public DateOnly DateOfBirth { get; private set; }

    public string Email { get; private set; }

    public string FirstName { get; private set; }

    public string HashedPassword { get; private set; }

    public string LastName { get; private set; }

    public string PhoneNumber { get; private set; }

    public UserType Type { get; private set; } = UserType.Client;

    #region Related Data

    public RefreshToken RefreshToken { get; private set; }

    public ICollection<UserModuleEntity> UserModules { get; set; } = [];

    #endregion Related Data

    #region Setters

    public void RemoveRefreshToken()
    {
        RefreshToken = null;
    }

    public void SetDateOfBirth(DateOnly dateOfBirth)
    {
        if (dateOfBirth.Year < 1900)
            throw new UserInvalidDateOfBirthException();

        DateOfBirth = dateOfBirth;
    }

    public void SetEmail(string email)
    {
        var propertyName = nameof(Email);
        var maxLength = StringLengthConst.LongString;

        if (string.IsNullOrWhiteSpace(email))
            throw new PropertyWasEmptyException(propertyName);

        if (email.Length > maxLength)
            throw new PropertyWasTooLongException(propertyName, maxLength);

        if (!IsValidEmail(email))
            throw new UserInvalidEmailFormatException();

        Email = email;
    }

    public void SetFirstName(string firstName)
    {
        var propertyName = nameof(FirstName);
        var maxLength = StringLengthConst.LongString;

        if (string.IsNullOrWhiteSpace(firstName))
            throw new PropertyWasEmptyException(propertyName);

        if (firstName.Length > maxLength)
            throw new PropertyWasTooLongException(propertyName, maxLength);

        FirstName = firstName;
    }

    public void SetHashedPassword(string hashedPassword)
    {
        var propertyName = nameof(HashedPassword);

        if (string.IsNullOrWhiteSpace(hashedPassword))
            throw new PropertyWasEmptyException(propertyName);

        HashedPassword = hashedPassword;
    }

    public void SetLastName(string lastName)
    {
        var propertyName = nameof(LastName);
        var maxLength = StringLengthConst.LongString;

        if (string.IsNullOrWhiteSpace(lastName))
            throw new PropertyWasEmptyException(propertyName);

        if (lastName.Length > maxLength)
            throw new PropertyWasTooLongException(propertyName, maxLength);

        LastName = lastName;
    }

    public void SetPhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            return;

        var propertyName = nameof(PhoneNumber);
        var maxLength = StringLengthConst.ShortString;

        if (phoneNumber.Length > maxLength)
            throw new PropertyWasTooLongException(propertyName, maxLength);

        PhoneNumber = phoneNumber;
    }

    public void SetRefreshToken(RefreshToken refreshToken)
    {
        if (refreshToken == null)
            throw new ArgumentNullException(nameof(refreshToken));

        if (RefreshToken == null)
        {
            RefreshToken = refreshToken.Clone();
            return;
        }

        RefreshToken.Update(refreshToken);
    }

    public void SetType(UserType type)
    {
        Type = type;
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

    #endregion Setters

    #region Methods

    public static User CreateSeed() => new User()
    {
        Id = new Guid(EntityIdConst.UserId),
        CreateTime = DateTime.MinValue,
        FirstName = "Super",
        LastName = "Admin",
        DateOfBirth = DateOnly.MinValue,
        Email = "superadmin@futureshop.pl",
        HashedPassword = "$2a$11$v1B9qwcIeH.PJLuFjnmK7O1Nu3TSUsc6oZ49.5DXOJhkIDcfzPD..", // Crypt.HashPassword("123456789"),
        Type = UserType.SuperAdmin
    };

    public void Update(User entity)
    {
        if (DateOfBirth != entity.DateOfBirth)
            DateOfBirth = entity.DateOfBirth;

        if (Email != entity.Email)
            Email = entity.Email;

        if (FirstName != entity.FirstName)
            FirstName = entity.FirstName;

        if (LastName != entity.LastName)
            LastName = entity.LastName;

        if (PhoneNumber != entity.PhoneNumber)
            PhoneNumber = entity.PhoneNumber;
    }

    #endregion Methods
}