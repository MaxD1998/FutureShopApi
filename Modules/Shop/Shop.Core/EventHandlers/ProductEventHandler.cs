using Shared.Core.Constans;
using Shared.Core.Dtos;
using Shared.Core.Enums;
using Shared.Core.Interfaces;
using Shop.Core.Dtos.Product;
using Shop.Core.EventServices;
using System.Text.Json;

namespace Shop.Core.EventHandlers;

public class ProductEventHandler(IProductEventService productEventService) : IMessageEventHandler
{
    private readonly IProductEventService _productEventService = productEventService;

    public string Exchange => RabbitMqExchangeConst.ProductModuleProduct;

    public string QueueName => "ShopModule-Product";

    public async Task ExecuteAsync(string message, CancellationToken cancellationToken)
    {
        var eventMessage = JsonSerializer.Deserialize<EventMessageDto>(message);

        switch (eventMessage.Type)
        {
            case MessageType.AddOrUpdate:
            {
                var productEvent = JsonSerializer.Deserialize<EventMessageDto<ProductEventDto>>(message);
                if (productEvent?.Message != null)
                    await _productEventService.CreateOrUpdateAsync(productEvent.Message, cancellationToken);

                break;
            }
            case MessageType.Delete:
            {
                var deleteEvent = JsonSerializer.Deserialize<EventMessageDto<Guid>>(message);
                if (deleteEvent?.Message != null && deleteEvent.Message != Guid.Empty)
                    await _productEventService.DeleteByIdAsync(deleteEvent.Message, cancellationToken);

                break;
            }
            default:
                throw new NotSupportedException($"Message type {eventMessage.Type} is not supported.");
        }
    }
}