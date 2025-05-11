using File.Core.Services;
using Shared.Core.Constans;
using Shared.Core.Dtos;
using Shared.Core.Enums;
using Shared.Core.Interfaces;
using System.Text.Json;

namespace File.Core.EventHandlers;

public class FilesToDeleteEventHandler(IFileService fileService) : IMessageEventHandler
{
    private readonly IFileService _fileService = fileService;

    public string Exchange => RabbitMqExchangeConst.ProductModuleFilesToDelete;

    public string QueueName => "FileModule-FilesToDelete";

    public Task ExecuteAsync(string message, CancellationToken cancellationToken)
    {
        var eventMessage = JsonSerializer.Deserialize<EventMessageDto>(message);

        switch (eventMessage.Type)
        {
            case MessageType.DeleteRange:
            {
                var deleteEvent = JsonSerializer.Deserialize<EventMessageDto<List<string>>>(message);
                if (deleteEvent?.Message != null && deleteEvent.Message.Count > 0)
                    _fileService.DeleteManyAsync(deleteEvent.Message, cancellationToken);

                break;
            }
            default:
                throw new NotSupportedException($"Message type {eventMessage.Type} is not supported.");
        }

        return Task.CompletedTask;
    }
}