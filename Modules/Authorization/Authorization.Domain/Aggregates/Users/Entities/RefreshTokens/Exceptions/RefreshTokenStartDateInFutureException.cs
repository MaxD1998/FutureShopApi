using Shared.Domain.Bases;
using System.Net;

namespace Authorization.Domain.Aggregates.Users.Entities.RefreshTokens.Exceptions;

public class RefreshTokenStartDateInFutureException : BaseException
{
    public override string ErrorMessage => "The refresh token start date cannot be in the future.";

    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}