namespace Shared.Infrastructure.Enums;

public enum UserType
{
    SuperAdmin = 0,

    LocalAdmin = 1,

    User = 2,

    Client = 3
}

public static class UserTypeExtension
{
    private static Dictionary<UserType, IEnumerable<UserType>> UserPrivilegesDictionary => new()
    {
        { UserType.SuperAdmin, new[] { UserType.SuperAdmin, UserType.LocalAdmin, UserType.User,  UserType.Client } },
        { UserType.LocalAdmin, new[] { UserType.LocalAdmin, UserType.User,  UserType.Client } },
        { UserType.User, new[] { UserType.User, UserType.Client} },
        { UserType.Client, new[] { UserType.Client} }
    };

    public static IEnumerable<UserType> GetUserPrivileges(this UserType type)
        => UserPrivilegesDictionary[type];
}