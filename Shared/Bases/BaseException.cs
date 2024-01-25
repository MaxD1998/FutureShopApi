using Shared.Dtos;

namespace Shared.Bases;

public abstract class BaseException : Exception
{
    public BaseException(ErrorMessageDto error)
    {
        Error = new ErrorDto(error);
    }

    public ErrorDto Error { get; }

    public abstract int StatusCode { get; }
}