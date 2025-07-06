using Microsoft.AspNetCore.Http;
using Shared.Infrastructure.Constants;
using System.Security.Claims;

namespace Shared.Core.Services;

public interface ICurrentUserService
{
    Guid? GetUserId();
}

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public Guid? GetUserId()
    {
        var id = _httpContextAccessor.HttpContext.User.FindFirstValue(JwtClaimNameConst.Id);

        return id is null || id == string.Empty ? null : new Guid(id);
    }
}