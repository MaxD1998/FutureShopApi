using Microsoft.AspNetCore.Http;
using Shared.Shared.Constants;
using System.Security.Claims;

namespace Shared.Core.Extensions;

public static class HttpContextAccessorExtension
{
    public static Guid? GetUserId(this IHttpContextAccessor httpContextAccessor)
    {
        var id = httpContextAccessor.HttpContext.User.FindFirstValue(JwtClaimNameConst.Id);

        return id is null || id == string.Empty ? null : new Guid(id);
    }
}