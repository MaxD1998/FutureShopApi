﻿using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Dtos;
using System.Net;

namespace Shared.Infrastructure.Exceptions;

public class ServiceUnavailableException : BaseException
{
    public ServiceUnavailableException(ErrorMessageDto error) : base(error)
    {
    }

    public override int StatusCode => (int)HttpStatusCode.ServiceUnavailable;
}