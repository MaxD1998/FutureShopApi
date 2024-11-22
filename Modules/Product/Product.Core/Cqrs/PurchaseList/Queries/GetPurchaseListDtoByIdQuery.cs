using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos.PurchaseList;
using Product.Domain.Entities;
using Product.Infrastructure;

namespace Product.Core.Cqrs.PurchaseList.Queries;
public record GetPurchaseListDtoByIdQuery(Guid Id) : IRequest<PurchaseListDto>;

internal class GetPurchaseListDtoByIdQueryHandler : IRequestHandler<GetPurchaseListDtoByIdQuery, PurchaseListDto>
{
    private readonly ProductPostgreSqlContext _context;

    public GetPurchaseListDtoByIdQueryHandler(ProductPostgreSqlContext context)
    {
        _context = context;
    }

    public async Task<PurchaseListDto> Handle(GetPurchaseListDtoByIdQuery request, CancellationToken cancellationToken)
        => await _context.Set<PurchaseListEntity>()
            .AsNoTracking()
            .Include(x => x.PurchaseListItems)
                .ThenInclude(x => x.Product)
                    .ThenInclude(x => x.ProductPhotos.OrderBy(y => y.Position).Take(1))
            .Where(x => x.Id == request.Id)
            .Select(x => new PurchaseListDto(x))
            .FirstOrDefaultAsync(cancellationToken);
}