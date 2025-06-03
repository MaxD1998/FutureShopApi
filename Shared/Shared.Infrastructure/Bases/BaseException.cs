using Shared.Infrastructure.Dtos;

namespace Shared.Infrastructure.Bases;

public abstract class BaseException : Exception
{
    public BaseException(ErrorDto error)
    {
        Error = error;
    }

    public ErrorDto Error { get; }

    public abstract int StatusCode { get; }
}