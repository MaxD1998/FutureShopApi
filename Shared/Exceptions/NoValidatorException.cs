using Microsoft.AspNetCore.Http;
using Shared.Bases;
using Shared.Dtos;

namespace Shared.Exceptions;

public class NoValidatorException : BaseException
{
    public NoValidatorException(ErrorMessageDto error) : base(error)
    {
    }

    public override int StatusCode => StatusCodes.Status501NotImplemented;
}