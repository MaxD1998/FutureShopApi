using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shop.Core.Dtos.Basket;
using Shop.Domain.Entities;
using Shop.Infrastructure;

namespace Shop.Core.Cqrs.Basket.Queries;
public record GetBasketDtoByIdQuery(Guid Id, Guid? FavouriteId) : IRequest<ResultDto<BasketDto>>;

internal class GetBasketDtoByIdQueryHandler : BaseService, IRequestHandler<GetBasketDtoByIdQuery, ResultDto<BasketDto>>
{
    private readonly ShopPostgreSqlContext _context;

    public GetBasketDtoByIdQueryHandler(ShopPostgreSqlContext context)
    {
        _context = context;
    }

    public async Task<ResultDto<BasketDto>> Handle(GetBasketDtoByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Set<BasketEntity>()
            .AsNoTracking()
            .Select(BasketDto.Map(x => x.PurchaseListId == request.FavouriteId))
            .FirstOrDefaultAsync(cancellationToken);

        return Success(result);
    }
}