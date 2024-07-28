using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos;
using Product.Domain.Entities;
using Product.Infrastructure;

namespace Product.Core.Cqrs.ProductBase.Queries;

public record GetListProductBaseIdNameDtoQuery : IRequest<IEnumerable<IdNameDto>>;

internal class GetsProductBaseIdNameDtoQueryHandler : IRequestHandler<GetListProductBaseIdNameDtoQuery, IEnumerable<IdNameDto>>
{
    private readonly ProductPostgreSqlContext _context;

    public GetsProductBaseIdNameDtoQueryHandler(ProductPostgreSqlContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<IdNameDto>> Handle(GetListProductBaseIdNameDtoQuery request, CancellationToken cancellationToken)
        => await _context.Set<ProductBaseEntity>()
            .AsNoTracking()
            .Select(x => new IdNameDto(x))
            .ToListAsync(cancellationToken);
}