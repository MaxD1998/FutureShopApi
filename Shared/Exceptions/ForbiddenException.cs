using Microsoft.AspNetCore.Http;
using Shared.Bases;
using Shared.Dtos;

namespace Shared.Exceptions;

public class ForbiddenException : BaseException
{
    public ForbiddenException(ErrorMessageDto error) : base(error)
    {
    }

    public override int StatusCode => StatusCodes.Status403Forbidden;
}