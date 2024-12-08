using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Domain.Entities;
using Product.Infrastructure;

namespace Product.Core.Cqrs.PurchaseList.Queries;
public record GetPurchaseListEntityByIdQuery(Guid Id) : IRequest<PurchaseListEntity>;

internal class GetPurchaseListEntityByIdQueryHandler : IRequestHandler<GetPurchaseListEntityByIdQuery, PurchaseListEntity>
{
    private readonly ProductPostgreSqlContext _context;

    public GetPurchaseListEntityByIdQueryHandler(ProductPostgreSqlContext context)
    {
        _context = context;
    }

    public async Task<PurchaseListEntity> Handle(GetPurchaseListEntityByIdQuery request, CancellationToken cancellationToken)
        => await _context.Set<PurchaseListEntity>()
            .AsNoTracking()
            .Include(x => x.PurchaseListItems)
            .FirstOrDefaultAsync(x => x.Id == request.Id);
}