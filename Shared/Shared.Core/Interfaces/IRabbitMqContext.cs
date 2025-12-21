namespace Shared.Core.Interfaces;

public interface IRabbitMqContext
{
    Task SendMessageAsync(string exchange, object body);
}