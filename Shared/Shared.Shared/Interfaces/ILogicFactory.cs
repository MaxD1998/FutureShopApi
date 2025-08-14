namespace Shared.Shared.Interfaces;

public interface ILogicFactory<TLogicFactory>
{
    Task<TResult> ExecuteAsync<TRequest, TResult>(TRequest request, Func<TLogicFactory, ILogic<TRequest, TResult>> factorySelector, CancellationToken cancellationToken);
}