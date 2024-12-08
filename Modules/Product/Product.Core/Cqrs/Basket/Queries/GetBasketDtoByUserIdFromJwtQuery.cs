using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos.Basket;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Extensions;

namespace Product.Core.Cqrs.Basket.Queries;
public record GetBasketDtoByUserIdFromJwtQuery : IRequest<BasketDto>;

internal class GetBasketDtoByUserIdFromJwtQueryHandler : IRequestHandler<GetBasketDtoByUserIdFromJwtQuery, BasketDto>
{
    private readonly ProductPostgreSqlContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetBasketDtoByUserIdFromJwtQueryHandler(ProductPostgreSqlContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<BasketDto> Handle(GetBasketDtoByUserIdFromJwtQuery request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.GetUserId();

        return await _context.Set<BasketEntity>()
            .AsNoTracking()
            .Include(x => x.BasketItems)
                .ThenInclude(x => x.Product)
                    .ThenInclude(x => x.ProductPhotos.Take(1))
            .Include(x => x.BasketItems)
                .ThenInclude(x => x.Product)
                    .ThenInclude(x => x.PurchaseListItems.Where(y => y.PurchaseList.UserId != null && y.PurchaseList.UserId == userId))
            .Where(x => x.UserId != null && x.UserId == userId)
            .Select(x => new BasketDto(x))
            .FirstOrDefaultAsync(cancellationToken);
    }
}