using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;
using Shop.Infrastructure;

namespace Shop.Core.Cqrs.PurchaseList.Queries;
public record GetPurchaseListEntityByIdQuery(Guid Id) : IRequest<PurchaseListEntity>;

internal class GetPurchaseListEntityByIdQueryHandler : IRequestHandler<GetPurchaseListEntityByIdQuery, PurchaseListEntity>
{
    private readonly ShopContext _context;

    public GetPurchaseListEntityByIdQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<PurchaseListEntity> Handle(GetPurchaseListEntityByIdQuery request, CancellationToken cancellationToken)
        => await _context.Set<PurchaseListEntity>()
            .AsNoTracking()
            .Include(x => x.PurchaseListItems)
            .FirstOrDefaultAsync(x => x.Id == request.Id);
}