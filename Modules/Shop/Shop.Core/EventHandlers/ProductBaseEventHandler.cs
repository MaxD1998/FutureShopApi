using MediatR;
using Shared.Core.Constans;
using Shared.Core.Dtos;
using Shared.Core.Enums;
using Shared.Core.Interfaces;
using Shop.Core.Cqrs.ProductBase.Commands;
using Shop.Core.Dtos.ProductBase;
using System.Text.Json;

namespace Shop.Core.EventHandlers;

public class ProductBaseEventHandler(IMediator mediator) : IMessageEventHandler
{
    private readonly IMediator _mediator = mediator;

    public string Exchange => RabbitMqExchangeConst.ProductModuleProductBase;

    public string QueueName => "ShopModule-ProductBase";

    public async Task ExecuteAsync(string message, CancellationToken cancellationToken)
    {
        var eventMessage = JsonSerializer.Deserialize<EventMessageDto>(message);

        switch (eventMessage.Type)
        {
            case MessageType.AddOrUpdate:
            {
                var productBaseEvent = JsonSerializer.Deserialize<EventMessageDto<ProductBaseEventDto>>(message);
                if (productBaseEvent?.Message != null)
                    await _mediator.Send(new CreateOrUpdateProductBaseEventDtoCommand(productBaseEvent.Message), cancellationToken);

                break;
            }
            case MessageType.Delete:
            {
                var deleteEvent = JsonSerializer.Deserialize<EventMessageDto<Guid>>(message);
                if (deleteEvent?.Message != null && deleteEvent.Message != Guid.Empty)
                    await _mediator.Send(new DeleteProductBaseByIdCommand(deleteEvent.Message), cancellationToken);

                break;
            }
            default:
                throw new NotSupportedException($"Message type {eventMessage.Type} is not supported.");
        }
    }
}