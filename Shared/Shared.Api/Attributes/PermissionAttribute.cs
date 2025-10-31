using Microsoft.AspNetCore.Authorization;
using Shared.Shared.Enums;

namespace Shared.Api.Attributes;

public class PermissionAttribute : AuthorizeAttribute
{
    public PermissionAttribute(params AuthorizationPermission[] permissions)
    {
        Policy = string.Join(",", permissions.Select(x => x.ToString()));
    }

    public PermissionAttribute(params WarehousePermission[] permissions)
    {
        Policy = string.Join(",", permissions.Select(x => x.ToString()));
    }

    public PermissionAttribute(params ShopPermission[] permissions)
    {
        Policy = string.Join(",", permissions.Select(x => x.ToString()));
    }

    public PermissionAttribute(params ProductPermission[] permissions)
    {
        Policy = string.Join(",", permissions.Select(x => x.ToString()));
    }
}