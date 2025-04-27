using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;
using Shop.Infrastructure;

namespace Shop.Core.Cqrs.Basket.Queries;
public record GetBasketEntityByIdQuery(Guid Id) : IRequest<BasketEntity>;

internal class GetBasketEntityByIdQueryHandler : IRequestHandler<GetBasketEntityByIdQuery, BasketEntity>
{
    private readonly ShopPostgreSqlContext _context;

    public GetBasketEntityByIdQueryHandler(ShopPostgreSqlContext context)
    {
        _context = context;
    }

    public async Task<BasketEntity> Handle(GetBasketEntityByIdQuery request, CancellationToken cancellationToken)
        => await _context.Set<BasketEntity>()
            .AsNoTracking()
            .Include(x => x.BasketItems)
            .FirstOrDefaultAsync(x => x.Id == request.Id);
}