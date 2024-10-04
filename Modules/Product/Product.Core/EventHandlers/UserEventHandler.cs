using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Interfaces;
using Shared.Infrastructure.Constants;
using System.Text.Json;

namespace Product.Core.EventHandlers;

public class UserEventHandler : IMessageEventHandler
{
    private readonly ProductPostgreSqlContext _context;

    public UserEventHandler(ProductPostgreSqlContext context)
    {
        _context = context;
    }

    public string QueueName => RabbitMqQeueNameConst.ProductUser;

    public async Task ExecuteAsync(string message, CancellationToken cancellationToken)
    {
        var user = JsonSerializer.Deserialize<UserEntity>(message);
        var entity = _context.Set<UserEntity>().FirstOrDefault(x => x.Id == user.Id);

        if (entity is null)
            await _context.Set<UserEntity>().AddAsync(user, cancellationToken);
        else
            entity.Update(user);

        await _context.SaveChangesAsync(cancellationToken);
    }
}