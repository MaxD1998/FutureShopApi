﻿using Shared.Domain.Enums;

namespace Shared.Domain.Extensions;

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