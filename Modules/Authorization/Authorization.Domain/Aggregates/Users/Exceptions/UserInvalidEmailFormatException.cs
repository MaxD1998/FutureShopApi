using Shared.Domain.Bases;
using System.Net;

namespace Authorization.Domain.Aggregates.Users.Exceptions;

public class UserInvalidEmailFormatException : BaseException
{
    public override string ErrorMessage => "Email is not in a valid format.";

    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}