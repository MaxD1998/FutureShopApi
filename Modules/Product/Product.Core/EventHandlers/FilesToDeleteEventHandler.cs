using Product.Core.EventServices;
using Shared.Core.Constans;
using Shared.Core.Dtos;
using Shared.Core.Enums;
using Shared.Core.Interfaces;
using System.Text.Json;

namespace Product.Core.EventHandlers;

public class FilesToDeleteEventHandler(IProductPhotoEventService productPhotoEventService) : IMessageEventHandler
{
    private readonly IProductPhotoEventService _productPhotoEventService = productPhotoEventService;

    public string Exchange => RabbitMqExchangeConst.FileModduleToDelete;

    public string QueueName => "ProductModule-FilesToDelete";

    public Task ExecuteAsync(string message, CancellationToken cancellationToken)
    {
        var eventMessage = JsonSerializer.Deserialize<EventMessageDto>(message);

        switch (eventMessage.Type)
        {
            case MessageType.CheckMissingFileIds:
            {
                var deleteEvent = JsonSerializer.Deserialize<EventMessageDto<List<string>>>(message);
                if (deleteEvent?.Message != null && deleteEvent.Message.Count > 0)
                    return _productPhotoEventService.GetMissingFileIdsAsync(deleteEvent.Message, cancellationToken);

                break;
            }
            default:
                throw new NotSupportedException($"Message type {eventMessage.Type} is not supported.");
        }

        return Task.CompletedTask;
    }
}