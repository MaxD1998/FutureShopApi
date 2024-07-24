using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Dtos;
using System.Net;

namespace Shared.Core.Exceptions;

public class NoValidatorException : BaseException
{
    public NoValidatorException(ErrorMessageDto error) : base(error)
    {
    }

    public override int StatusCode => (int)HttpStatusCode.NotImplemented;
}