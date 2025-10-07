using Shared.Infrastructure.Bases;
using System.Net;

namespace Shared.Infrastructure.Exceptions;

public class PropertyWasTooLongException : BaseException
{
    private readonly int _length;
    private readonly string _propertyName;

    public PropertyWasTooLongException(string propertyName, int length)
    {
        _propertyName = propertyName;
        _length = length;
    }

    public override string ErrorMessage => $"The property \'{_propertyName}\' exceeds the maximum allowed length of {_length} characters.";

    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}