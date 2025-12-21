using Shared.Shared.Bases;
using System.Net;

namespace Authorization.Domain.Exceptions.RefreshTokens;

public class RefreshTokenStartDateInFutureException : BaseException
{
    public override string ErrorMessage => "The refresh token start date cannot be in the future.";

    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}