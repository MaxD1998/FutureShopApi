namespace Shared.Shared.Interfaces;

public interface ILogic<in TRequest>
{
    Task ExecuteAsync(TRequest request, CancellationToken cancellationToken);
}

public interface ILogic<in TRequest, TResult>
{
    Task<TResult> ExecuteAsync(TRequest request, CancellationToken cancellationToken);
}