using Microsoft.AspNetCore.Authorization;
using Shared.Infrastructure.Enums;

namespace Shared.Api.Attributes;

public class RoleAttribute : AuthorizeAttribute
{
    public RoleAttribute(UserType userType)
    {
        Roles = string.Join(",", userType.GetUserPrivileges());
    }
}