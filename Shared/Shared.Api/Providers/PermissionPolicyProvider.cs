using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Shared.Api.Requirements;

namespace Shared.Api.Providers;

public class PermissionPolicyProvider : DefaultAuthorizationPolicyProvider
{
    public PermissionPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
    {
    }

    public override Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
    {
        var permissions = policyName.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        if (permissions.Length > 0)
        {
            var policy = new AuthorizationPolicyBuilder()
                .AddRequirements(new PermissionRequirement(permissions))
                .Build();

            return Task.FromResult(policy);
        }

        return base.GetPolicyAsync(policyName);
    }
}