using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos.PurchaseList;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Extensions;

namespace Product.Core.Cqrs.PurchaseList.Queries;

public record GetListPurchaseListDtoByUserIdFromJwtQuery : IRequest<IEnumerable<PurchaseListDto>>;

internal class GetListPurchaseListDtoByUserIdFromJwtQueryHandler : IRequestHandler<GetListPurchaseListDtoByUserIdFromJwtQuery, IEnumerable<PurchaseListDto>>
{
    private readonly ProductPostgreSqlContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetListPurchaseListDtoByUserIdFromJwtQueryHandler(ProductPostgreSqlContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IEnumerable<PurchaseListDto>> Handle(GetListPurchaseListDtoByUserIdFromJwtQuery request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.GetUserId();

        return await _context.Set<PurchaseListEntity>()
            .AsNoTracking()
            .Include(x => x.PurchaseListItems)
                .ThenInclude(x => x.Product)
                    .ThenInclude(x => x.ProductPhotos.OrderBy(y => y.Position).Take(1))
            .Where(x => x.UserId != null && x.UserId == userId)
            .OrderByDescending(x => x.IsFavourite)
                .ThenBy(x => x.Name)
            .Select(x => new PurchaseListDto(x))
            .ToListAsync(cancellationToken);
    }
}