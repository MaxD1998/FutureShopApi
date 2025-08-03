using Shared.Domain.Bases;
using System.Net;

namespace Shop.Domain.Aggregates.AdCampaigns.Exceptions;

public class AdCampaignActivationRequiresItemsException : BaseException
{
    public override string ErrorMessage => "Ad campaign activation requires at least one ad campaign item.";

    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}