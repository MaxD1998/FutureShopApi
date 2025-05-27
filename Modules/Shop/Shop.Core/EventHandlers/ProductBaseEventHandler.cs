using Shared.Core.Constans;
using Shared.Core.Dtos;
using Shared.Core.Enums;
using Shared.Core.Interfaces;
using Shop.Core.Dtos.ProductBase;
using Shop.Core.EventServices;
using System.Text.Json;

namespace Shop.Core.EventHandlers;

public class ProductBaseEventHandler(IProductBaseEventService productBaseEventService) : IMessageEventHandler
{
    private readonly IProductBaseEventService _productBaseEventService = productBaseEventService;

    public string Exchange => RabbitMqExchangeConst.ProductModuleProductBase;

    public string QueueName => "ShopModule-ProductBase";

    public Task ExecuteAsync(string message, CancellationToken cancellationToken)
    {
        var eventMessage = JsonSerializer.Deserialize<EventMessageDto>(message);

        switch (eventMessage.Type)
        {
            case MessageType.AddOrUpdate:
            {
                var productBaseEvent = JsonSerializer.Deserialize<EventMessageDto<ProductBaseEventDto>>(message);
                if (productBaseEvent?.Message != null)
                    return _productBaseEventService.CreateOrUpdateAsync(productBaseEvent.Message, cancellationToken);

                break;
            }
            case MessageType.Delete:
            {
                var deleteEvent = JsonSerializer.Deserialize<EventMessageDto<Guid>>(message);
                if (deleteEvent?.Message != null && deleteEvent.Message != Guid.Empty)
                    return _productBaseEventService.DeleteByExternalIdAsync(deleteEvent.Message, cancellationToken);

                break;
            }
            default:
                throw new NotSupportedException($"Message type {eventMessage.Type} is not supported.");
        }

        return Task.CompletedTask;
    }
}