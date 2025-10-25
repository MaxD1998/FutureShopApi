using Shared.Core.Constans;
using Shared.Core.Dtos;
using Shared.Core.Enums;
using Shared.Core.Interfaces;
using Shop.Core.Dtos.User;
using Shop.Core.EventServices;
using System.Text.Json;

namespace Shop.Core.EventHandlers;

public class UserEventHandler(IUserEventService userEventService) : IMessageEventHandler
{
    private readonly IUserEventService _userEventService = userEventService;

    public string Exchange => RabbitMqExchangeConst.AuthorizationModuleUser;

    public string QueueName => "ShopModule-User";

    public Task ExecuteAsync(string message, CancellationToken cancellationToken)
    {
        var eventMessage = JsonSerializer.Deserialize<EventMessageDto>(message);

        switch (eventMessage.Type)
        {
            case MessageType.AddOrUpdate:
            {
                var userEvent = JsonSerializer.Deserialize<EventMessageDto<UserEventDto>>(message);
                if (userEvent?.Message != null)
                    return _userEventService.CreateOrUpdateAsync(userEvent.Message, cancellationToken);

                break;
            }
            case MessageType.Delete:
            {
                var deleteEvent = JsonSerializer.Deserialize<EventMessageDto<Guid>>(message);
                if (deleteEvent?.Message != null && deleteEvent.Message != Guid.Empty)
                    return _userEventService.DeleteByExternalIdAsync(deleteEvent.Message, cancellationToken);

                break;
            }
            default:
                throw new NotSupportedException($"Message type {eventMessage.Type} is not supported.");
        }

        return Task.CompletedTask;
    }
}