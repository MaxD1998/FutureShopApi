using System.Net;

namespace Shared.Domain.Bases;

public abstract class BaseException : Exception
{
    public abstract string ErrorMessage { get; }

    public abstract HttpStatusCode StatusCode { get; }
}