using Shared.Shared.Bases;
using System.Net;

namespace Authorization.Domain.Exceptions.Users;

public class UserInvalidEmailFormatException : BaseException
{
    public override string ErrorMessage => "Email is not in a valid format.";

    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}