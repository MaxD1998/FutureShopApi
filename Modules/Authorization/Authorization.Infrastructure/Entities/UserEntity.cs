using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Enums;
using Shared.Infrastructure.Interfaces;

namespace Authorization.Infrastructure.Entities;

public class UserEntity : BaseEntity, IUpdate<UserEntity>
{
    public DateOnly DateOfBirth { get; set; }

    public string Email { get; set; }

    public string FirstName { get; set; }

    public string HashedPassword { get; set; }

    public string LastName { get; set; }

    public string PhoneNumber { get; set; }

    public UserType Type { get; set; } = UserType.Client;

    #region Related Data

    public RefreshTokenEntity RefreshToken { get; set; }

    public ICollection<UserModuleEntity> UserModules { get; set; } = [];

    #endregion Related Data

    #region Methods

    public void Update(UserEntity entity) => throw new NotImplementedException();

    #endregion Methods
}