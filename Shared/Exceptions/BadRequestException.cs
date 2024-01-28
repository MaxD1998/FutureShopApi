using Microsoft.AspNetCore.Http;
using Shared.Bases;
using Shared.Dtos;

namespace Shared.Exceptions;

public class BadRequestException : BaseException
{
    public BadRequestException(ErrorMessageDto error) : base(error)
    {
    }

    public override int StatusCode => StatusCodes.Status400BadRequest;
}