using Shared.Infrastructure.Bases;
using System.Net;

namespace Authorization.Infrastructure.Exceptions.RefreshTokens;

public class RefreshTokenEndDateInPastException : BaseException
{
    public override string ErrorMessage => "The refresh token end date cannot be in the past.";

    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}