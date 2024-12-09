using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Shared.Api.Results;

public class NotImplementedObjectResult : ObjectResult
{
    public NotImplementedObjectResult(object value) : base(value)
    {
        StatusCode = (int)HttpStatusCode.NotImplemented;
    }
}