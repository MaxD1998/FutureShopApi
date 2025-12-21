using Shared.Shared.Bases;
using System.Net;

namespace Shop.Infrastructure.Persistence.Exceptions.AdCampaigns;

public class AdCampaignActivationRequiresItemsException : BaseException
{
    public override string ErrorMessage => "Ad campaign activation requires at least one ad campaign item.";

    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}