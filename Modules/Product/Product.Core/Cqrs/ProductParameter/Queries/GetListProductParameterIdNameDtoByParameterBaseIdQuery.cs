using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos;
using Product.Domain.Entities;
using Product.Infrastructure;

namespace Product.Core.Cqrs.ProductParameter.Queries;

public record GetListProductParameterIdNameDtoByParameterBaseIdQuery(Guid Id) : IRequest<IEnumerable<IdNameDto>>;

internal class GetsProductParameterIdNameDtoByParameterBaseIdQueryHandler : IRequestHandler<GetListProductParameterIdNameDtoByParameterBaseIdQuery, IEnumerable<IdNameDto>>
{
    private readonly ProductPostgreSqlContext _context;

    public GetsProductParameterIdNameDtoByParameterBaseIdQueryHandler(ProductPostgreSqlContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<IdNameDto>> Handle(GetListProductParameterIdNameDtoByParameterBaseIdQuery request, CancellationToken cancellationToken)
        => await _context.Set<ProductParameterEntity>()
            .AsNoTracking()
            .Where(x => x.ProductBaseId == request.Id)
            .Select(x => new IdNameDto(x))
            .ToListAsync(cancellationToken);
}