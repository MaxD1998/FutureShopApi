using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Core.Extensions;
using Shop.Core.Dtos.Basket;
using Shop.Domain.Entities;
using Shop.Infrastructure;

namespace Shop.Core.Cqrs.Basket.Queries;
public record GetBasketDtoByUserIdFromJwtQuery : IRequest<ResultDto<BasketDto>>;

internal class GetBasketDtoByUserIdFromJwtQueryHandler : BaseService, IRequestHandler<GetBasketDtoByUserIdFromJwtQuery, ResultDto<BasketDto>>
{
    private readonly ShopPostgreSqlContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetBasketDtoByUserIdFromJwtQueryHandler(ShopPostgreSqlContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ResultDto<BasketDto>> Handle(GetBasketDtoByUserIdFromJwtQuery request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.GetUserId();
        var result = await _context.Set<BasketEntity>()
            .AsNoTracking()
            .Where(x => x.UserId != null && x.UserId == userId)
            .Select(BasketDto.Map(x => x.PurchaseList.UserId != null && x.PurchaseList.UserId == userId))
            .FirstOrDefaultAsync(cancellationToken);

        return Success(result);
    }
}