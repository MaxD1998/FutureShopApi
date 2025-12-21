using Shared.Shared.Bases;
using Shop.Domain.Entities.AdCampaigns;
using System.Net;

namespace Shop.Domain.Exceptions.AdCampaigns;

public class AdCampaignItemsMustBeUniqueException : BaseException
{
    public override string ErrorMessage => $"Ad campaign items must have unique '{nameof(AdCampaignItemEntity.Lang)}' property.";

    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}