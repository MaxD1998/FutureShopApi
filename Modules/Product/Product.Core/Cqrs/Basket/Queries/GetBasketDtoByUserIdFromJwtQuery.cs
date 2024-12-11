using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos.Basket;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Core.Extensions;

namespace Product.Core.Cqrs.Basket.Queries;
public record GetBasketDtoByUserIdFromJwtQuery : IRequest<ResultDto<BasketDto>>;

internal class GetBasketDtoByUserIdFromJwtQueryHandler : BaseService, IRequestHandler<GetBasketDtoByUserIdFromJwtQuery, ResultDto<BasketDto>>
{
    private readonly ProductPostgreSqlContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetBasketDtoByUserIdFromJwtQueryHandler(ProductPostgreSqlContext context, IHttpContextAccessor httpContextAccessor)
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