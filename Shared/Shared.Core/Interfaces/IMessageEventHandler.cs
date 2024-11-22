namespace Shared.Core.Interfaces;

public interface IMessageEventHandler
{
    string QueueName { get; }

    Task ExecuteAsync(string message, CancellationToken cancellationToken);
}