namespace Shop.Core.Interfaces;

public interface ILogic<TResult>
{
    Task<TResult> ExecuteAsync(CancellationToken cancellationToken);
}

public interface ILogic<TRequest, TResult>
{
    Task<TResult> ExecuteAsync(TRequest request, CancellationToken cancellationToken);
}