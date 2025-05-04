using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shop.Core.Dtos.PurchaseList;
using Shop.Domain.Entities;
using Shop.Infrastructure;

namespace Shop.Core.Cqrs.PurchaseList.Queries;
public record GetPurchaseListDtoByIdQuery(Guid Id) : IRequest<ResultDto<PurchaseListDto>>;

internal class GetPurchaseListDtoByIdQueryHandler : BaseService, IRequestHandler<GetPurchaseListDtoByIdQuery, ResultDto<PurchaseListDto>>
{
    private readonly ShopContext _context;

    public GetPurchaseListDtoByIdQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<ResultDto<PurchaseListDto>> Handle(GetPurchaseListDtoByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Set<PurchaseListEntity>()
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .Select(PurchaseListDto.Map())
            .FirstOrDefaultAsync(cancellationToken);

        return Success(result);
    }
}