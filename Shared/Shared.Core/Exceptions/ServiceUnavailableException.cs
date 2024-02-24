using Shared.Core.Bases;
using Shared.Core.Dtos;
using System.Net;

namespace Shared.Core.Exceptions;

public class ServiceUnavailableException : BaseException
{
    public ServiceUnavailableException(ErrorMessageDto error) : base(error)
    {
    }

    public override int StatusCode => (int)HttpStatusCode.ServiceUnavailable;
}