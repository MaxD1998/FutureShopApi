using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shop.Core.Dtos.ProductBase;
using Shop.Domain.Entities;
using Shop.Infrastructure;

namespace Shop.Core.Cqrs.ProductBase.Commands;

public record CreateOrUpdateProductBaseEventDtoCommand(ProductBaseEventDto Dto) : IRequest<ResultDto>;

internal class CreateOrUpdateProductBaseEventDtoCommandHandler(ShopPostgreSqlContext context) : BaseService, IRequestHandler<CreateOrUpdateProductBaseEventDtoCommand, ResultDto>
{
    private readonly ShopPostgreSqlContext _context = context;

    public async Task<ResultDto> Handle(CreateOrUpdateProductBaseEventDtoCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<ProductBaseEntity>()
            .FirstOrDefaultAsync(x => x.ExternalId == request.Dto.Id, cancellationToken);

        var eventEntity = request.Dto.Map(_context);

        if (entity is null)
            await _context.Set<ProductBaseEntity>().AddAsync(eventEntity, cancellationToken);
        else
            entity.UpdateEvent(eventEntity);

        await _context.SaveChangesAsync(cancellationToken);

        return Success();
    }
}