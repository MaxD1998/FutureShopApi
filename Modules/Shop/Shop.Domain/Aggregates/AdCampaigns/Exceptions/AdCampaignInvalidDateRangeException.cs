using Shared.Domain.Bases;
using Shared.Shared.Helpers;
using System.Net;

namespace Shop.Domain.Aggregates.AdCampaigns.Exceptions;

public class AdCampaignInvalidDateRangeException : BaseException
{
    private readonly DateTime _end;
    private readonly DateTime _start;

    public AdCampaignInvalidDateRangeException(DateTime start, DateTime end)
    {
        _start = start;
        _end = end;
    }

    public override string ErrorMessage => $"Invalid date range. Start can't be grater than end. {nameof(AdCampaignAggregate.Start)} was {_start.ToString(TimeFormat.DefaultTimeFormat)}. {nameof(AdCampaignAggregate.End)} was {_end.ToString(TimeFormat.DefaultTimeFormat)}";

    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}