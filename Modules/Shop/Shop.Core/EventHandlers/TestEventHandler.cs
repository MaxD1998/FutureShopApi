using Shared.Core.Interfaces;

namespace Shop.Core.EventHandlers;

public class TestEventHandler : IMessageEventHandler
{
    public string QueueName => "Test";

    public Task ExecuteAsync(string message, CancellationToken cancellationToken)
    {
        Console.WriteLine(message);
        return Task.CompletedTask;
    }
}