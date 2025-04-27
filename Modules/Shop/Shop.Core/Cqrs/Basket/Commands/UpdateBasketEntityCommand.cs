using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Core.Errors;
using Shop.Domain.Entities;
using Shop.Infrastructure;
using System.Net;

namespace Shop.Core.Cqrs.Basket.Commands;
public record UpdateBasketEntityCommand(Guid Id, BasketEntity Entity) : IRequest<ResultDto<BasketEntity>>;

internal class UpdateBasketEntityCommandHandler : BaseService, IRequestHandler<UpdateBasketEntityCommand, ResultDto<BasketEntity>>
{
    private readonly ShopPostgreSqlContext _context;

    public UpdateBasketEntityCommandHandler(ShopPostgreSqlContext context)
    {
        _context = context;
    }

    public async Task<ResultDto<BasketEntity>> Handle(UpdateBasketEntityCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<BasketEntity>()
            .Include(x => x.BasketItems)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
            return Error<BasketEntity>(HttpStatusCode.NotFound, CommonExceptionMessage.C007RecordWasNotFound);

        entity.Update(request.Entity);

        await _context.SaveChangesAsync(cancellationToken);

        return Success(entity);
    }
}