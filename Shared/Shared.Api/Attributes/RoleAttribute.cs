using Microsoft.AspNetCore.Authorization;
using Shared.Domain.Enums;

namespace Shared.Api.Attributes;

public class RoleAttribute : AuthorizeAttribute
{
    public RoleAttribute(UserType userType)
    {
        Roles = string.Join(",", userType.GetUserPrivileges());
    }
}