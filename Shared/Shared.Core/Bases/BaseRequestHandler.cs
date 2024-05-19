using MediatR;
using Shared.Core.Errors;
using Shared.Core.Exceptions;
using Shared.Infrastructure.Bases;

namespace Shared.Core.Bases;

public abstract class BaseRequestHandler<TContext, TRequest> : IRequestHandler<TRequest>
    where TContext : BaseContext
    where TRequest : IRequest
{
    protected readonly TContext _context;

    public BaseRequestHandler(TContext context)
    {
        if (!context.Database.CanConnect())
            throw new ServiceUnavailableException(ExceptionMessage.DatabaseNotAvailable);

        _context = context;
    }

    public abstract Task Handle(TRequest request, CancellationToken cancellationToken);
}

public abstract class BaseRequestHandler<TContext, TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TContext : BaseContext
    where TRequest : IRequest<TResponse>
{
    protected readonly TContext _context;

    public BaseRequestHandler(TContext context)
    {
        if (!context.Database.CanConnect())
            throw new ServiceUnavailableException(ExceptionMessage.DatabaseNotAvailable);

        _context = context;
    }

    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}