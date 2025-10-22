using Microsoft.AspNetCore.Http;
using Shared.Infrastructure.Constants;
using Shared.Shared.Extensions;
using System.Security.Claims;

namespace Shared.Core.Services;

public interface ICurrentUserService
{
    Guid? GetUserId();
}

internal class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public Guid? GetUserId()
    {
        var id = _httpContextAccessor.HttpContext.User.FindFirstValue(JwtClaimNameConst.Id);

        return id.ToNullableGuid();
    }
}