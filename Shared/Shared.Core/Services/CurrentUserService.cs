using Microsoft.AspNetCore.Http;
using Shared.Shared.Constants;
using Shared.Shared.Extensions;
using System.Security.Claims;
using Shared.Core.Interfaces.Services;

namespace Shared.Core.Services;

internal class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public Guid? GetUserId()
    {
        var id = _httpContextAccessor.HttpContext.User.FindFirstValue(JwtClaimNameConst.Id);

        return id.ToNullableGuid();
    }

    public bool IsRecordOwner(Guid inputId)
        => GetUserId() == inputId;
}