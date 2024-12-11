using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Core.Errors;
using System.Net;

namespace Product.Core.Cqrs.Basket.Commands;
public record UpdateBasketEntityCommand(Guid Id, BasketEntity Entity) : IRequest<ResultDto<BasketEntity>>;

internal class UpdateBasketEntityCommandHandler : BaseService, IRequestHandler<UpdateBasketEntityCommand, ResultDto<BasketEntity>>
{
    private readonly ProductPostgreSqlContext _context;

    public UpdateBasketEntityCommandHandler(ProductPostgreSqlContext context)
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