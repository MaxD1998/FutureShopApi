namespace Shared.Core.Interfaces;

public interface IMessageEventHandler
{
    string Exchange { get; }

    string QueueName { get; }

    Task ExecuteAsync(string message, CancellationToken cancellationToken);
}