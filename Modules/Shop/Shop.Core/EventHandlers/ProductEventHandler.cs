using MediatR;
using Shared.Core.Constans;
using Shared.Core.Dtos;
using Shared.Core.Enums;
using Shared.Core.Interfaces;
using Shop.Core.Cqrs.Product.Commands;
using Shop.Core.Dtos.Product;
using System.Text.Json;

namespace Shop.Core.EventHandlers;

public class ProductEventHandler(IMediator mediator) : IMessageEventHandler
{
    private readonly IMediator _mediator = mediator;

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
                    await _mediator.Send(new CreateOrUpdateProductEventDtoCommand(productEvent.Message), cancellationToken);

                break;
            }
            case MessageType.Delete:
            {
                var deleteEvent = JsonSerializer.Deserialize<EventMessageDto<Guid>>(message);
                if (deleteEvent?.Message != null && deleteEvent.Message != Guid.Empty)
                    await _mediator.Send(new DeleteProductByIdCommand(deleteEvent.Message), cancellationToken);

                break;
            }
            default:
                throw new NotSupportedException($"Message type {eventMessage.Type} is not supported.");
        }
    }
}