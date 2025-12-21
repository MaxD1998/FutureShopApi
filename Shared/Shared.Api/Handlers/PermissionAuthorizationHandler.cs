using Microsoft.AspNetCore.Authorization;
using Shared.Api.Requirements;
using Shared.Shared.Constants;
using Shared.Domain.Enums;
using Shared.Shared.Constants;
using System.Security.Claims;

namespace Shared.Api.Handlers;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        var userRole = context.User.Claims
            .Where(c => c.Type == ClaimTypes.Role)
            .Select(c => c.Value)
            .FirstOrDefault();

        if (!Enum.TryParse<UserType>(userRole, out var role))
            return Task.CompletedTask;

        switch (role)
        {
            case UserType.SuperAdmin:
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            case UserType.Employee:
            {
                var userPermissions = context.User.Claims
                    .Where(c => c.Type == JwtClaimNameConst.Permissions)
                    .Select(c => c.Value)
                    .ToHashSet();

                if (requirement.Permissions.Any(userPermissions.Contains))
                    context.Succeed(requirement);

                return Task.CompletedTask;
            }

            default:
                return Task.CompletedTask;
        }
    }
}