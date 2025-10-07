using Shared.Infrastructure.Bases;
using System.Net;

namespace Shared.Infrastructure.Exceptions;

public class PropertyWasNegativeException : BaseException
{
    private readonly string _propertyName;

    public PropertyWasNegativeException(string propertyName)
    {
        _propertyName = propertyName;
    }

    public override string ErrorMessage => $"The property \' {_propertyName} \' was negative.";

    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}