using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos.PurchaseList;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;
using Shared.Core.Dtos;

namespace Product.Core.Cqrs.PurchaseList.Queries;
public record GetPurchaseListDtoByIdQuery(Guid Id) : IRequest<ResultDto<PurchaseListDto>>;

internal class GetPurchaseListDtoByIdQueryHandler : BaseService, IRequestHandler<GetPurchaseListDtoByIdQuery, ResultDto<PurchaseListDto>>
{
    private readonly ProductPostgreSqlContext _context;

    public GetPurchaseListDtoByIdQueryHandler(ProductPostgreSqlContext context)
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