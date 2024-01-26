using Domain.Enums;
using Shared.Bases;

namespace Domain.Entities;

public class UserEntity : BaseEntity
{
    public DateOnly DateOfBirth { get; set; }

    public string Email { get; set; }

    public string FirsName { get; set; }

    public string HashedPassword { get; set; }

    public string LastName { get; set; }

    public string PhoneNumber { get; set; }

    public UserType Type { get; set; }

    #region Related Data

    public RefreshTokenEntity RefreshToken { get; set; }

    #endregion Related Data
}