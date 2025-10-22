using Shared.Infrastructure.Bases;
using Shop.Infrastructure.Entities.AdCampaigns;
using System.Net;

namespace Shop.Infrastructure.Exceptions.AdCampaigns;

public class AdCampaignItemsMustBeUniqueException : BaseException
{
    public override string ErrorMessage => $"Ad campaign items must have unique '{nameof(AdCampaignItemEntity.Lang)}' property.";

    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}