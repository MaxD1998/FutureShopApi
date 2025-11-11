using Shared.Infrastructure.Bases;
using System.Net;

namespace Shared.Infrastructure.Exceptions;

public class RecordWasNotFoundException : BaseException
{
    private readonly string _recordId;

    public RecordWasNotFoundException(Guid recordId)
    {
        _recordId = recordId.ToString();
    }

    public override string ErrorMessage => $"The record with Id \'{_recordId}\' cannot be found.";

    public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
}