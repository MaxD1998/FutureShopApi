using Authorization.Domain.Aggregates.Users.Entities;
using Authorization.Domain.Aggregates.Users.Entities.RefreshTokens;
using Authorization.Domain.Aggregates.Users.Exceptions;
using Shared.Domain.Bases;
using Shared.Domain.Constants;
using Shared.Domain.Enums;
using Shared.Domain.Interfaces;

namespace Authorization.Domain.Aggregates.Users;

public class UserAggregate : BaseEntity, IUpdate<UserAggregate>
{
    public UserAggregate(DateOnly dateOfBirth, string email, string firstName, string hashedPassword, string lastName)
    {
        SetDateOfBirth(dateOfBirth);
        SetEmail(email);
        SetFirstName(firstName);
        SetHashedPassword(hashedPassword);
        SetLastName(lastName);
    }

    public UserAggregate(DateOnly dateOfBirth, string email, string firstName, string hashedPassword, string lastName, string phoneNumber)
    {
        SetDateOfBirth(dateOfBirth);
        SetEmail(email);
        SetFirstName(firstName);
        SetHashedPassword(hashedPassword);
        SetLastName(lastName);
        SetPhoneNumber(phoneNumber);
    }

    private UserAggregate()
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

    public RefreshTokenEntity RefreshToken { get; private set; }

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
        ValidateRequiredLongStringProperty(nameof(Email), email);

        if (!IsValidEmail(email))
            throw new UserInvalidEmailFormatException();

        Email = email;
    }

    public void SetFirstName(string firstName)
    {
        ValidateRequiredLongStringProperty(nameof(FirstName), firstName);

        FirstName = firstName;
    }

    public void SetHashedPassword(string hashedPassword)
    {
        ValidateRequiredProperty(nameof(HashedPassword), hashedPassword);

        HashedPassword = hashedPassword;
    }

    public void SetLastName(string lastName)
    {
        ValidateRequiredLongStringProperty(nameof(LastName), lastName);

        LastName = lastName;
    }

    public void SetPhoneNumber(string phoneNumber)
    {
        ValidateShortStringProperty(nameof(PhoneNumber), phoneNumber);

        if (phoneNumber == string.Empty)
            phoneNumber = null;

        PhoneNumber = phoneNumber;
    }

    public void SetRefreshToken(RefreshTokenEntity refreshToken)
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

    public static UserAggregate CreateSeed() => new UserAggregate()
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

    public void Update(UserAggregate entity)
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

        RefreshToken = entity.RefreshToken?.Clone();
    }

    #endregion Methods
}