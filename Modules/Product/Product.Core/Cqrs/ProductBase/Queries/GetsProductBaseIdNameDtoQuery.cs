using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;

namespace Product.Core.Cqrs.ProductBase.Queries;

public record GetsProductBaseIdNameDtoQuery : IRequest<IEnumerable<IdNameDto>>;

internal class GetsProductBaseIdNameDtoQueryHandler : BaseRequestHandler<ProductContext, GetsProductBaseIdNameDtoQuery, IEnumerable<IdNameDto>>
{
    public GetsProductBaseIdNameDtoQueryHandler(ProductContext context) : base(context)
    {
    }

    public override async Task<IEnumerable<IdNameDto>> Handle(GetsProductBaseIdNameDtoQuery request, CancellationToken cancellationToken)
        => await _context.Set<ProductBaseEntity>()
            .AsNoTracking()
            .Select(x => new IdNameDto(x))
            .ToListAsync(cancellationToken);
}