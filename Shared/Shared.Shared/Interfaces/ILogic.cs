namespace Shared.Shared.Interfaces;

public interface ILogic<TRequest>
{
    Task ExecuteAsync(TRequest request, CancellationToken cancellationToken);
}

public interface ILogic<TRequest, TResult>
{
    Task<TResult> ExecuteAsync(TRequest request, CancellationToken cancellationToken);
}