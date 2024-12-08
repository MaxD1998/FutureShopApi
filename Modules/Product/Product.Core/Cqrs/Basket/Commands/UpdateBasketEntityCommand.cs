using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Errors;
using Shared.Core.Exceptions;

namespace Product.Core.Cqrs.Basket.Commands;
public record UpdateBasketEntityCommand(Guid Id, BasketEntity Entity) : IRequest<BasketEntity>;

internal class UpdateBasketEntityCommandHandler : IRequestHandler<UpdateBasketEntityCommand, BasketEntity>
{
    private readonly ProductPostgreSqlContext _context;

    public UpdateBasketEntityCommandHandler(ProductPostgreSqlContext context)
    {
        _context = context;
    }

    public async Task<BasketEntity> Handle(UpdateBasketEntityCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<BasketEntity>()
            .Include(x => x.BasketItems)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
            throw new NotFoundException(CommonExceptionMessage.C007RecordWasNotFound);

        entity.Update(request.Entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}