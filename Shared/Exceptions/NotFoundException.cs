using Microsoft.AspNetCore.Http;
using Shared.Bases;
using Shared.Dtos;

namespace Shared.Exceptions;

public class NotFoundException : BaseException
{
    public NotFoundException(ErrorMessageDto error) : base(error)
    {
    }

    public override int StatusCode => StatusCodes.Status404NotFound;
}