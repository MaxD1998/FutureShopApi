using Shared.Domain.Bases;
using System.Net;

namespace Authorization.Domain.Aggregates.Users.Exceptions;

public class UserInvalidDateOfBirthException : BaseException
{
    public override string ErrorMessage => "Invalid user's date of birth. Year has to be grater or equal than 1900.";

    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}