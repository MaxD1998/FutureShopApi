using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;

namespace Product.Core.Cqrs.ProductParameter.Queries;

public record GetsProductParameterIdNameDtoByParameterBaseIdQuery(Guid Id) : IRequest<IEnumerable<IdNameDto>>;

internal class GetsProductParameterIdNameDtoByParameterBaseIdQueryHandler : BaseRequestHandler<ProductContext, GetsProductParameterIdNameDtoByParameterBaseIdQuery, IEnumerable<IdNameDto>>
{
    public GetsProductParameterIdNameDtoByParameterBaseIdQueryHandler(ProductContext context) : base(context)
    {
    }

    public override async Task<IEnumerable<IdNameDto>> Handle(GetsProductParameterIdNameDtoByParameterBaseIdQuery request, CancellationToken cancellationToken)
        => await _context.Set<ProductParameterEntity>()
            .AsNoTracking()
            .Where(x => x.ProductBaseId == request.Id)
            .Select(x => new IdNameDto(x))
            .ToListAsync(cancellationToken);
}