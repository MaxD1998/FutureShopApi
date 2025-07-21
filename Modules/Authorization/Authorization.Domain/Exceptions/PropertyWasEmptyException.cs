using Shared.Domain.Bases;
using System.Net;

namespace Authorization.Domain.Exceptions;

public class PropertyWasEmptyException : BaseException
{
    private readonly string _propertyName;

    public PropertyWasEmptyException(string propertyName)
    {
        _propertyName = propertyName;
    }

    public override string ErrorMessage => $"The property '{_propertyName}' cannot be empty.";

    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}