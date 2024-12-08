using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Domain.Entities;
using Product.Infrastructure;

namespace Product.Core.Cqrs.Basket.Queries;
public record GetBasketEntityByIdQuery(Guid Id) : IRequest<BasketEntity>;

internal class GetBasketEntityByIdQueryHandler : IRequestHandler<GetBasketEntityByIdQuery, BasketEntity>
{
    private readonly ProductPostgreSqlContext _context;

    public GetBasketEntityByIdQueryHandler(ProductPostgreSqlContext context)
    {
        _context = context;
    }

    public async Task<BasketEntity> Handle(GetBasketEntityByIdQuery request, CancellationToken cancellationToken)
        => await _context.Set<BasketEntity>()
            .AsNoTracking()
            .Include(x => x.BasketItems)
            .FirstOrDefaultAsync(x => x.Id == request.Id);
}