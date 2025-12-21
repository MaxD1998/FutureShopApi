using Shared.Shared.Bases;
using System.Net;

namespace Shared.Domain.Exceptions;

public class PropertyWasOutOfRangeException : BaseException
{
    private readonly object _max;
    private readonly object _min;
    private readonly string _propertyName;

    public PropertyWasOutOfRangeException(string propertyName, object min, object max)
    {
        _propertyName = propertyName;
        _min = min;
        _max = max;
    }

    public override string ErrorMessage => $"The property '{_propertyName}' was out of range. Expected between {_min} and {_max}.";

    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}