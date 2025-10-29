using Microsoft.AspNetCore.Authorization;

namespace Shared.Api.Requirements;

public class PermissionRequirement : IAuthorizationRequirement
{
    public PermissionRequirement(IEnumerable<string> permissions)
    {
        Permissions = permissions;
    }

    public IEnumerable<string> Permissions { get; }
}