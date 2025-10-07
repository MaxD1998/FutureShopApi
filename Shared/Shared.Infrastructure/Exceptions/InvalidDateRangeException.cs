using Shared.Infrastructure.Bases;
using Shared.Shared.Helpers;
using System.Net;

namespace Shared.Infrastructure.Exceptions;

public class InvalidDateRangeException : BaseException
{
    private readonly DateTime _end;
    private readonly DateTime _start;

    public InvalidDateRangeException(DateTime start, DateTime end)
    {
        _start = start;
        _end = end;
    }

    public override string ErrorMessage => $"Invalid date range. Start can't be grater than end. Start was {_start.ToString(TimeFormat.DefaultTimeFormat)}. End was {_end.ToString(TimeFormat.DefaultTimeFormat)}";

    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}