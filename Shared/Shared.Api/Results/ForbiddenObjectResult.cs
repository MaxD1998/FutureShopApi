using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Shared.Api.Results;

public class ForbiddenObjectResult : ObjectResult
{
    public ForbiddenObjectResult(object value) : base(value)
    {
        StatusCode = (int)HttpStatusCode.Forbidden;
    }
}