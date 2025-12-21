using Shared.Shared.Bases;
using System.Net;

namespace Shared.Infrastructure.Exceptions;

public class ServiceUnavailableException : BaseException
{
    public override string ErrorMessage => "Database is not available";

    public override HttpStatusCode StatusCode => HttpStatusCode.ServiceUnavailable;
}