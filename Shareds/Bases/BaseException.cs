using Shareds.Dtos;

namespace Shareds.Bases;

public abstract class BaseException : Exception
{
    public BaseException(ErrorMessageDto error)
    {
        Error = new ErrorDto(error);
    }

    public ErrorDto Error { get; }

    public abstract int StatusCode { get; }
}