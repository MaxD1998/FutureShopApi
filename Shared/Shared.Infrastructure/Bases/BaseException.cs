using Shared.Infrastructure.Dtos;

namespace Shared.Infrastructure.Bases;

public abstract class BaseException : Exception
{
    public BaseException(ErrorMessageDto error)
    {
        Error = new ErrorDto(error);
    }

    public ErrorDto Error { get; }

    public abstract int StatusCode { get; }
}