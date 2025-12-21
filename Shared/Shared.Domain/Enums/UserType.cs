namespace Shared.Domain.Enums;

public enum UserType
{
    SuperAdmin = 0,

    Employee = 1,

    Customer = 2
}

public static class UserTypeExtension
{
    private static Dictionary<UserType, List<UserType>> UserPrivilegesDictionary => new()
    {
        { UserType.SuperAdmin, new List<UserType> { UserType.SuperAdmin, UserType.Employee, UserType.Customer } },
        { UserType.Employee, new List<UserType> { UserType.Employee, UserType.Customer} },
        { UserType.Customer, new List<UserType> { UserType.Customer} }
    };

    public static List<UserType> GetUserPrivileges(this UserType type)
        => UserPrivilegesDictionary[type];
}