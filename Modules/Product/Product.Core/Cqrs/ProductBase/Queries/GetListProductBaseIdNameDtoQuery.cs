using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;
using Shared.Core.Dtos;

namespace Product.Core.Cqrs.ProductBase.Queries;

public record GetListProductBaseIdNameDtoQuery : IRequest<ResultDto<IEnumerable<IdNameDto>>>;

internal class GetsProductBaseIdNameDtoQueryHandler : BaseService, IRequestHandler<GetListProductBaseIdNameDtoQuery, ResultDto<IEnumerable<IdNameDto>>>
{
    private readonly ProductPostgreSqlContext _context;

    public GetsProductBaseIdNameDtoQueryHandler(ProductPostgreSqlContext context)
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