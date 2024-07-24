using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Dtos;
using System.Net;

namespace Shared.Core.Exceptions;

public class BadRequestException : BaseException
{
    public BadRequestException(ErrorMessageDto error) : base(error)
    {
    }

    public override int StatusCode => (int)HttpStatusCode.BadRequest;
}