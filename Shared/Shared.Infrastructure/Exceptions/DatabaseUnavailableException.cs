using Shared.Domain.Bases;
using System.Net;

namespace Shared.Infrastructure.Exceptions;

public class DatabaseUnavailableException : BaseException
{
    public override string ErrorMessage => "Database is unavailable.";

    public override HttpStatusCode StatusCode => HttpStatusCode.ServiceUnavailable;
}