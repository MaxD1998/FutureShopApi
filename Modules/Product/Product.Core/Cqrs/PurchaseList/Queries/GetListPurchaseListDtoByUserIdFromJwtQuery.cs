using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos.PurchaseList;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Core.Extensions;

namespace Product.Core.Cqrs.PurchaseList.Queries;

public record GetListPurchaseListDtoByUserIdFromJwtQuery : IRequest<ResultDto<IEnumerable<PurchaseListDto>>>;

internal class GetListPurchaseListDtoByUserIdFromJwtQueryHandler : BaseService, IRequestHandler<GetListPurchaseListDtoByUserIdFromJwtQuery, ResultDto<IEnumerable<PurchaseListDto>>>
{
    private readonly ProductPostgreSqlContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetListPurchaseListDtoByUserIdFromJwtQueryHandler(ProductPostgreSqlContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ResultDto<IEnumerable<PurchaseListDto>>> Handle(GetListPurchaseListDtoByUserIdFromJwtQuery request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.GetUserId();
        var results = await _context.Set<PurchaseListEntity>()
            .AsNoTracking()
            .Where(x => x.UserId != null && x.UserId == userId)
            .OrderByDescending(x => x.IsFavourite)
                .ThenBy(x => x.Name)
            .Select(PurchaseListDto.Map())
            .ToListAsync(cancellationToken);

        return Success<IEnumerable<PurchaseListDto>>(results);
    }
}