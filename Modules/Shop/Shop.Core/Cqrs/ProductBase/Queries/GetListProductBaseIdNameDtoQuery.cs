using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shop.Core.Dtos;
using Shop.Domain.Entities;
using Shop.Infrastructure;

namespace Shop.Core.Cqrs.ProductBase.Queries;

public record GetListProductBaseIdNameDtoQuery : IRequest<ResultDto<IEnumerable<IdNameDto>>>;

internal class GetsProductBaseIdNameDtoQueryHandler : BaseService, IRequestHandler<GetListProductBaseIdNameDtoQuery, ResultDto<IEnumerable<IdNameDto>>>
{
    private readonly ShopContext _context;

    public GetsProductBaseIdNameDtoQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<ResultDto<IEnumerable<IdNameDto>>> Handle(GetListProductBaseIdNameDtoQuery request, CancellationToken cancellationToken)
    {
        var results = await _context.Set<ProductBaseEntity>()
            .AsNoTracking()
            .Select(IdNameDto.MapFromProductBase())
            .ToListAsync(cancellationToken);

        return Success<IEnumerable<IdNameDto>>(results);
    }
}