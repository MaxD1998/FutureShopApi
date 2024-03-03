namespace Shared.Domain.Enums;

public enum UserType
{
    SuperAdmin = 0,
    LocalAdmin = 1,
    Manager = 2,
    Employee = 3,
    Client = 4
}

public static class UserTypeExtension
{
    private static Dictionary<UserType, IEnumerable<UserType>> _userPrivilegesDictionary => new Dictionary<UserType, IEnumerable<UserType>>()
    {
        { UserType.SuperAdmin, new[] { UserType.SuperAdmin, UserType.LocalAdmin, UserType.Manager, UserType.Employee, UserType.Client } },
        { UserType.LocalAdmin, new[] { UserType.LocalAdmin, UserType.Manager, UserType.Employee, UserType.Client } },
        { UserType.Manager, new[] { UserType.Manager, UserType.Employee, UserType.Client} },
        { UserType.Employee, new[] { UserType.Employee, UserType.Client} },
        { UserType.Client, new[] { UserType.Client} }
    };

    public static IEnumerable<UserType> GetUserPrivileges(this UserType type)
        => _userPrivilegesDictionary[type];
}