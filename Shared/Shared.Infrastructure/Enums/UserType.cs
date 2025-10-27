namespace Shared.Infrastructure.Enums;

public enum UserType
{
    SuperAdmin = 0,

    Employee = 1,

    Customer = 2
}

public static class UserTypeExtension
{
    private static Dictionary<UserType, IEnumerable<UserType>> UserPrivilegesDictionary => new()
    {
        { UserType.SuperAdmin, new[] { UserType.SuperAdmin, UserType.Employee,  UserType.Customer } },
        { UserType.Employee, new[] { UserType.Employee, UserType.Customer} },
        { UserType.Customer, new[] { UserType.Customer} }
    };

    public static IEnumerable<UserType> GetUserPrivileges(this UserType type)
        => UserPrivilegesDictionary[type];
}