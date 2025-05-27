using Shared.Core.Constans;
using Shared.Core.Dtos;
using Shared.Core.Enums;
using Shared.Core.Interfaces;
using Shop.Core.Dtos.Category;
using Shop.Core.EventServices;
using System.Text.Json;

namespace Shop.Core.EventHandlers;

public class CategoryEventHandler(ICategoryEventService categoryEventService) : IMessageEventHandler
{
    private readonly ICategoryEventService _categoryEventService = categoryEventService;

    public string Exchange => RabbitMqExchangeConst.ProductModuleCategory;

    public string QueueName => "ShopModule-Category";

    public Task ExecuteAsync(string message, CancellationToken cancellationToken)
    {
        var eventMessage = JsonSerializer.Deserialize<EventMessageDto>(message);

        switch (eventMessage.Type)
        {
            case MessageType.AddOrUpdate:
            {
                var categoryEvent = JsonSerializer.Deserialize<EventMessageDto<CategoryEventDto>>(message);
                if (categoryEvent?.Message != null)
                    return _categoryEventService.CreateOrUpdateAsync(categoryEvent.Message, cancellationToken);

                break;
            }
            case MessageType.Delete:
            {
                var deleteEvent = JsonSerializer.Deserialize<EventMessageDto<Guid>>(message);
                if (deleteEvent?.Message != null && deleteEvent.Message != Guid.Empty)
                    return _categoryEventService.DeleteByExternalIdAsync(deleteEvent.Message, cancellationToken);

                break;
            }
            default:
                throw new NotSupportedException($"Message type {eventMessage.Type} is not supported.");
        }

        return Task.CompletedTask;
    }
}