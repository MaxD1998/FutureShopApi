using Shared.Core.Enums;

namespace Shared.Core.Dtos;

public class EventMessageDto
{
    public MessageType Type { get; set; }

    public static EventMessageDto<T> Create<T>(T message, MessageType type) => EventMessageDto<T>.Create(message, type);
}

public class EventMessageDto<T> : EventMessageDto
{
    public EventMessageDto()
    {
    }

    private EventMessageDto(T message, MessageType type)
    {
        Message = message;
        Type = type;
    }

    public T Message { get; set; }

    public static EventMessageDto<T> Create(T message, MessageType type) => new EventMessageDto<T>(message, type);
}