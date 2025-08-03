using Shared.Domain.Bases;
using Shared.Shared.Helpers;
using System.Net;

namespace Shop.Domain.Aggregates.AdCampaigns.Exceptions;

public class AdCampaignStartBeforeAllowedException : BaseException
{
    private readonly DateTime _start;

    public AdCampaignStartBeforeAllowedException(DateTime start)
    {
        _start = start;
    }

    public override string ErrorMessage => $"Start date was before allowed. Minimum allowed date is \"01.01.1900\". {nameof(AdCampaignAggregate.Start)} was {_start.ToString(TimeFormat.DefaultTimeFormat)}";

    public override HttpStatusCode StatusCode => throw new NotImplementedException();
}