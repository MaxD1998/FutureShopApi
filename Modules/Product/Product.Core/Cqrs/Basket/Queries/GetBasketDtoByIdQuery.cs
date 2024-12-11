using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos.Basket;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;
using Shared.Core.Dtos;

namespace Product.Core.Cqrs.Basket.Queries;
public record GetBasketDtoByIdQuery(Guid Id, Guid? FavouriteId) : IRequest<ResultDto<BasketDto>>;

internal class GetBasketDtoByIdQueryHandler : BaseService, IRequestHandler<GetBasketDtoByIdQuery, ResultDto<BasketDto>>
{
    private readonly ProductPostgreSqlContext _context;

    public GetBasketDtoByIdQueryHandler(ProductPostgreSqlContext context)
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