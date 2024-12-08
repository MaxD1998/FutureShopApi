using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos.Basket;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Extensions;

namespace Product.Core.Cqrs.Basket.Queries;
public record GetBasketDtoByIdQuery(Guid Id, Guid? FavouriteId) : IRequest<BasketDto>;

internal class GetBasketDtoByIdQueryHandler : IRequestHandler<GetBasketDtoByIdQuery, BasketDto>
{
    private readonly ProductPostgreSqlContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetBasketDtoByIdQueryHandler(ProductPostgreSqlContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<BasketDto> Handle(GetBasketDtoByIdQuery request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.GetUserId();

        return await _context.Set<BasketEntity>()
            .AsNoTracking()
            .Include(x => x.BasketItems)
                .ThenInclude(x => x.Product)
                    .ThenInclude(x => x.ProductPhotos.Take(1))
            .Include(x => x.BasketItems)
                .ThenInclude(x => x.Product)
                    .ThenInclude(x => x.PurchaseListItems.Where(y => y.PurchaseListId == request.FavouriteId))
            .Where(x => x.Id == request.Id)
            .Select(x => new BasketDto(x))
            .FirstOrDefaultAsync(cancellationToken);
    }
}