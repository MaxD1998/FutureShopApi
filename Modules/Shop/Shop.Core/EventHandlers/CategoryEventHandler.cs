using MediatR;
using Shared.Core.Constans;
using Shared.Core.Dtos;
using Shared.Core.Enums;
using Shared.Core.Interfaces;
using Shop.Core.Cqrs.Category.Commands;
using Shop.Core.Dtos.Category;
using System.Text.Json;

namespace Shop.Core.EventHandlers;

public class CategoryEventHandler(IMediator mediator) : IMessageEventHandler
{
    private readonly IMediator _mediator = mediator;

    public string Exchange => RabbitMqExchangeConst.ProductModuleCategory;

    public string QueueName => "ShopModule-Category";

    public async Task ExecuteAsync(string message, CancellationToken cancellationToken)
    {
        var eventMessage = JsonSerializer.Deserialize<EventMessageDto>(message);

        switch (eventMessage.Type)
        {
            case MessageType.AddOrUpdate:
            {
                var categoryEvent = JsonSerializer.Deserialize<EventMessageDto<CategoryEventDto>>(message);
                if (categoryEvent?.Message != null)
                    await _mediator.Send(new CreateOrUpdateCategoryEventDtoCommand(categoryEvent.Message), cancellationToken);

                break;
            }
            case MessageType.Delete:
            {
                var deleteEvent = JsonSerializer.Deserialize<EventMessageDto<Guid>>(message);
                if (deleteEvent?.Message != null && deleteEvent.Message != Guid.Empty)
                    await _mediator.Send(new DeleteCategoryByIdCommand(deleteEvent.Message), cancellationToken);

                break;
            }
            default:
                throw new NotSupportedException($"Message type {eventMessage.Type} is not supported.");
        }
    }
}